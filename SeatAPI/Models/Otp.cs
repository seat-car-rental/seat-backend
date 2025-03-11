using System.ComponentModel.DataAnnotations;

namespace SeatAPI.Models
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; } = false;

        // Foreign Key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
