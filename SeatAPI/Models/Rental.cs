using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatAPI.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool DeliveredToRenter { get; set; } // Delivery or Pickup

        // Foreign Key (Renter)
        [Required]
        public int RenterId { get; set; }
        public User Renter { get; set; }

        // Foreign Key (Car)
        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }

        // Navigation Property
        public Payment Payment { get; set; }
    }
}
