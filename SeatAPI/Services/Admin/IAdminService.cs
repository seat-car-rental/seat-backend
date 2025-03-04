using SeatAPI.Models;

namespace SeatAPI.Services.Admin
{
    public interface IAdminService
    {
        List<User> GetAllUsers();
        bool ChangeUserRole(int userId, string newRole);
        bool DeleteUser(int userId);
    }
}
