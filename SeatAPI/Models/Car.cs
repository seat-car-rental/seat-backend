using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Kind { get; set; } // Sedan, SUV, etc.

        public int GasolineCapacity { get; set; }

        public string Engine { get; set; }

        [Required]
        public string GearType { get; set; } // Manual, Automatic

        public string Description { get; set; }

        [Required]
        public string CarPicture { get; set; }

        // Documents
        public string CarLicense { get; set; }
        public string RideLicense { get; set; }
        public string Insurance { get; set; } // Optional

        public bool HasDriver { get; set; }

        // Foreign Key (Owner)
        [Required]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        // Navigation Property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
