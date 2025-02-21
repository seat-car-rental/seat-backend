using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }

        public string ProfilePic { get; set; } =  string.Empty;

        public bool Verified { get; set; } = false;

        [Required]
        public string Role { get; set; } // User, Admin, CustomerService

        // Navigation Properties
        public ICollection<Car> OwnedCars { get; set; } = new List<Car>();
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
