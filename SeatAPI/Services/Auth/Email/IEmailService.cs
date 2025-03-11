using SeatAPI.DTOS.Authentication;

namespace SeatAPI.Services.Auth.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailReciverDto request);
    }
}
