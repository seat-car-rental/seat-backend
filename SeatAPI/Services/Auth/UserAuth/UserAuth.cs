using SeatAPI.Constants;
using SeatAPI.Data;
using SeatAPI.DTOS.User;
using SeatAPI.Models;
using SeatAPI.Services.Auth.PasswordHasher;

namespace SeatAPI.Services.Auth.UserAuth
{
    public class UserAuth(IPasswordHasher _passwordHasher, ApplicationDbContext _context) : IUserAuth
    {
        public void RegisterUser(UserRegisterDto userDto)
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
                Role = ClintRoles.User
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            return _passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        
    }
}
