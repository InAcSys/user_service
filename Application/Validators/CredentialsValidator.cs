using FluentValidation;
using UserService.Domain.DTOs.User;

namespace UserService.Application.Validators
{
    public class CredentialsValidator : AbstractValidator<CredentialDTO>
    {
        public CredentialsValidator()
        {
            RuleFor(credentials => credentials.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.");
            RuleFor(credentials => credentials.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
        }
    }
}