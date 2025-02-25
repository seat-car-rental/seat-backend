using Microsoft.IdentityModel.Tokens;
using SeatAPI.Configurations;
using SeatAPI.Constants;
using SeatAPI.Data;
using SeatAPI.DTOS.Authentication;
using SeatAPI.DTOS.User;
using SeatAPI.Models;
using SeatAPI.Services.Auth.PasswordHasher;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SeatAPI.Services.Auth.UserAuth
{
    public class UserAuth(IPasswordHasher _passwordHasher, ApplicationDbContext _context , JwtSettings _jwtSettings) : IUserAuth
    {
        //public void RegisterUser(UserRegisterDto userDto)
        //{
        //    _passwordHasher.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        //    var user = new User
        //    {
        //        FirstName = userDto.FirstName,
        //        LastName = userDto.LastName,
        //        Phone = userDto.Phone,
        //        Email = userDto.Email,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt,
        //        Role = ClintRoles.User
        //    };

        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //}

        //public bool Login(string email, string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (user == null) return false;

        //    return _passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        //}

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthResponseDto RegisterUser(UserRegisterDto userDto)
        {
            _passwordHasher.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = ClintRoles.User,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            string token = GenerateJwtToken(user);
            return new AuthResponseDto { Token = token, Expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes) };
        }

        public AuthResponseDto Login(UserLoginDto userLoginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userLoginDto.Email);
            if (user == null || !_passwordHasher.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); 
            _context.SaveChanges();

            string token = GenerateJwtToken(user);
            return new AuthResponseDto { Token = token, Expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes) };
        }

        public AuthResponseDto RefreshToken(RefreshTokenDto tokenDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == tokenDto.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token.");
            }

            string token = GenerateJwtToken(user);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _context.SaveChanges();

            return new AuthResponseDto { Token = token, Expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes) };
        }
    }
}
