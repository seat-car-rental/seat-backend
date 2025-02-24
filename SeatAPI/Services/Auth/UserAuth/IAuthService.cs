using SeatAPI.DTOS.Authentication;
using SeatAPI.DTOS.User;

namespace SeatAPI.Services.Auth.UserAuth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Authenticate(UserLoginDto loginDto);

    }
}
