namespace SeatAPI.DTOS.Authentication
{
    public class VerifyOtpDto
    {
        
        public int UserId { get; set; }
        public string OtpCode { get; set; }
    }
}
