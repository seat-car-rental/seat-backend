using System.ComponentModel.DataAnnotations;

namespace SeatAPI.DTOS.User
{
    public class UserRegisterDto
    {

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public required string Password { get; set; }
    }
}
