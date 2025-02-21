using SeatAPI.DTOS.User;
using SeatAPI.Models;

namespace SeatAPI.Services.Auth.UserAuth
{
    public interface IUserAuth
    {
        void RegisterUser(UserRegisterDto userRegisterDto);
        bool Login(string email, string password);
       

    }
}