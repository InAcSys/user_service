using FluentValidation;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstNames)
                .NotEmpty()
                .WithMessage("First names are required.")
                .Length(2, 255)
                .WithMessage("First names must be between 2 and 255 characters.");
            RuleFor(user => user.LastNames)
                .NotEmpty()
                .WithMessage("Last names are required.")
                .Length(2, 255)
                .WithMessage("Last names must be between 2 and 255 characters.");
            RuleFor(user => user.ShortName)
                .NotEmpty()
                .WithMessage("Short name is required.")
                .Length(2, 100)
                .WithMessage("Short name must be between 2 and 100 characters.");
            RuleFor(user => user.Code)
                .NotEmpty()
                .WithMessage("Code is required.")
                .MinimumLength(2)
                .WithMessage("Code must be at least 2 characters long.");
            RuleFor(user => user.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .Length(2, 255)
                .WithMessage("Address must be between 2 and 255 characters.");
            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Phone number must be a valid format.");
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.");
            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
            RuleFor(user => user.CI)
                .NotEmpty()
                .WithMessage("CI is required.")
                .Length(2, 50)
                .WithMessage("CI must be between 2 and 50 characters.");
            RuleFor(user => user.ImageUrl)
                .NotEmpty()
                .WithMessage("Image URL is required.")
                .Matches(@"^(http|https)://[^\s]+$")
                .WithMessage("Image URL must be a valid URL.");
        }
    }
}