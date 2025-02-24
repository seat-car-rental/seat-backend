
using Microsoft.AspNetCore.Mvc;
using SeatAPI.DTOS.User;
using SeatAPI.Services.Auth.UserAuth;

namespace SeatAPI.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await _authService.Authenticate(loginDto);
            if (result == null) return Unauthorized("Invalid credentials");

            return Ok(result);
        }
    }
}
