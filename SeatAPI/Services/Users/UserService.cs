using Microsoft.EntityFrameworkCore;
using SeatAPI.Data;
using SeatAPI.Models;

namespace SeatAPI.Services.Users
{
    public class UserService(ApplicationDbContext _context) : IUserService
    {
        public User? GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            return user;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        
    }
}
