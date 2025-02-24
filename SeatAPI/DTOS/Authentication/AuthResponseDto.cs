namespace SeatAPI.DTOS.Authentication
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
