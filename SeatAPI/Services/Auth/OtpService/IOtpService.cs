namespace SeatAPI.Services.Auth.OtpService
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(int userId);
        Task<bool> VerifyOtpAsync(int userId, string otpCode);
    }
    
}
