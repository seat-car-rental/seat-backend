using SeatAPI.DTOS.User;

namespace SeatAPI.Services.Auth.UserAuth
{
    public interface IUserAuth
    {
        void RegisterUser(UserRegisterDto userRegisterDto);
        void Login(string email, string password);

        //Task<UserRegisterDto> GetUserByIdAsync(int id);
        //Task<bool> CreateUserAsync(UserRegisterDto userDto);
    }
}
