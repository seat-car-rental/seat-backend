using Microsoft.EntityFrameworkCore;
using SeatAPI.Data;
using SeatAPI.DTOS.Authentication;
using SeatAPI.Models;
using SeatAPI.Services.Auth.Email;

namespace SeatAPI.Services.Auth.OtpService
{
    public class OtpService(ApplicationDbContext _context , IEmailService _emailService) :IOtpService
    {
        private readonly Random _random = new();

        public async Task<string> GenerateOtpAsync(int userId)
        {
            string otpCode = _random.Next(100000, 999999).ToString();
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found");
            var otp = new Otp
            {
                Code = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                UserId = userId
            };

            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            // Send OTP via Email
            string subject = "Your OTP Code";
            string body = $"Your OTP code is <b>{otpCode}</b>. It will expire in 5 minutes.";
            EmailReciverDto reciver = new EmailReciverDto
            {
                To = user.Email,
                Subject = subject,
                Body = body
            };
            await _emailService.SendEmailAsync(reciver);

            return otpCode;
        }

        public async Task<bool> VerifyOtpAsync(int userId, string otpCode)
        {
            var otp = await _context.Otps
                .Where(o => o.UserId == userId && o.Code == otpCode && o.ExpiryTime > DateTime.UtcNow && !o.IsUsed)
                .FirstOrDefaultAsync();

            if (otp == null)
                return false;

            otp.IsUsed = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
