using SeatAPI.Models;

namespace SeatAPI.Services.Users
{
    public interface IUserService
    {
        User? GetUserByEmail(string Email);
        List<User> GetAllUsers();
        
    }
}
