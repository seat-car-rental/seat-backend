using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatAPI.DTOS.Authentication;
using SeatAPI.Services.Auth.OtpService;

namespace SeatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController(IOtpService _otpService) : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOtp([FromBody] int userId)
        {
            var otp = await _otpService.GenerateOtpAsync(userId);
            return Ok(new { OtpCode = otp });
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto request)
        {
            var isValid = await _otpService.VerifyOtpAsync(request.UserId, request.OtpCode);
            if (!isValid)
                return BadRequest("Invalid or expired OTP");

            return Ok("OTP verified successfully");
        }
    }
}
