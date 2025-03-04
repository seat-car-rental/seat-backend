
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeatAPI.DTOS.Authentication;
using SeatAPI.DTOS.User;
using SeatAPI.Services.Auth.UserAuth;

namespace SeatAPI.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService _authService , IUserAuth _userAuth , IValidator<UserRegisterDto> _registerValidator , IValidator<UserLoginDto> _loginValidator) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            var validationResult = await _registerValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = _userAuth.RegisterUser(userDto);
            if (response == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {

            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
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
