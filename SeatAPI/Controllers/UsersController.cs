using Microsoft.AspNetCore.Mvc;
using SeatAPI.DTOS.User;
using SeatAPI.Services.Auth.UserAuth;


namespace SeatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserAuth _userAuth) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {
            _userAuth.RegisterUser(userDto);
            return Ok(new { message = "User registered successfully" });
        }

    }
}
