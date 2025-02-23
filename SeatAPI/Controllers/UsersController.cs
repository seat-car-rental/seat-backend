using Microsoft.AspNetCore.Mvc;
using SeatAPI.DTOS.User;
using SeatAPI.Models;
using SeatAPI.Services.Auth.UserAuth;
using SeatAPI.Services.Users;


namespace SeatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserAuth _userAuth , IUserService _userService) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {
            _userAuth.RegisterUser(userDto);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpGet("login")]
        public IActionResult Login(string username, string password)
        {
            _userAuth.Login(username, password);
            return Ok(new { message = "User Login successfully" });
        }

        [HttpGet("get-user-by-email")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            return _userService.GetUserByEmail(email);
        }

    }
}
