using FluentValidation;
using SeatAPI.DTOS.User;
using System.Text.RegularExpressions;

namespace SeatAPI.Validations
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters long.")
                .Matches(@"^[A-Za-z]").WithMessage("First Name must start with a letter.")
                .MaximumLength(50).WithMessage("First Name must be at most 50 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long.")
                .Matches(@"^[A-Za-z]").WithMessage("Last Name must start with a letter.")
                .MaximumLength(50).WithMessage("Last Name must be at most 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Example: amin@gmail.com");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.Phone)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^(011|012|015)\d{8}$").WithMessage("Phone number must start with 011, 012, or 015 and be exactly 11 digits long.");
        }
    }
}
