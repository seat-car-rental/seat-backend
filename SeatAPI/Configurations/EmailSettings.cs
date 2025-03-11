namespace SeatAPI.Configurations
{
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public required int SmtpPort { get; set; }
        public required string SenderEmail { get; set; }
        public required string SenderPassword { get; set; }
    }
}
