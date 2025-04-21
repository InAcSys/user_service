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
                .WithMessage("Password is required.");
        }
    }
}