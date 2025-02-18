using SeatAPI.Models;
using SeatAPI.Data;
using SeatAPI.Services.Auth;

namespace SeatAPI.Services.Users
{
    public class UserService(IPasswordHasher _passwordHasher, ApplicationDbContext _context) : IUserService
    {
        public void RegisterUser(User user, string password)
        {
            _passwordHasher.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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
