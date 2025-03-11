using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SeatAPI.Configurations;
using SeatAPI.DTOS.Authentication;

namespace SeatAPI.Services.Auth.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailReciverDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("MyApp", _emailSettings.SenderEmail));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;

                email.Body = new TextPart("html") { Text = request.Body };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }
    }
}
