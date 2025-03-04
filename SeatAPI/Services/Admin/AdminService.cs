using SeatAPI.Data;
using SeatAPI.Models;

namespace SeatAPI.Services.Admin
{
    public class AdminService(ApplicationDbContext _context ) : IAdminService
    {
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool ChangeUserRole(int userId, string newRole)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            user.Role = newRole;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

    }
}
