using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeatAPI.Data;
using SeatAPI.DTOS.Authentication;
using SeatAPI.DTOS.User;
using SeatAPI.Models;
using SeatAPI.Services.Auth.UserAuth;
using SeatAPI.Services.Users;
using System.Security.Claims;


namespace SeatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ApplicationDbContext _context , IUserAuth _userAuth , IUserService _userService) : ControllerBase
    {

        [HttpGet("get-user-by-email")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null || user.Role != "User")
            {
                return BadRequest("User Not Found");
            }
            
            return Ok(_userService.GetUserByEmail(email));
        }


    }
}
