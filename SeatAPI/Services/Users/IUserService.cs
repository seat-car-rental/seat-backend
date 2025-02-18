using SeatAPI.Models;

namespace SeatAPI.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(User user, string password);
        bool Login(string email, string password);
    }
}
