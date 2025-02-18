using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatAPI.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; } = "Pending"; // Paid, Released

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Foreign Key (Rental)
        [Required]
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}
