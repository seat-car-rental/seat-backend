using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatAPI.Services.Admin;

namespace SeatAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController(IAdminService _adminService) : ControllerBase
    {
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("change-role/{userId}")]
        public IActionResult ChangeUserRole(int userId, [FromBody] string newRole)
        {
            bool success = _adminService.ChangeUserRole(userId, newRole);
            if (!success) return NotFound("User not found");

            return Ok(new { message = $"User role changed to {newRole}" });
        }

        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            bool success = _adminService.DeleteUser(userId);
            if (!success) return NotFound("User not found");

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
