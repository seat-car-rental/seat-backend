
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeatAPI.DTOS.Authentication;
using SeatAPI.DTOS.User;
using SeatAPI.Services.Auth.UserAuth;

namespace SeatAPI.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService _authService , IUserAuth _userAuth) : ControllerBase
    {

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {
            var response = _userAuth.RegisterUser(userDto);
            if (response == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var response = _userAuth.Login(loginDto);
            if (response == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto tokenDto)
        {
            try
            {
                var response = _userAuth.RefreshToken(tokenDto);
                return Ok(response);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
