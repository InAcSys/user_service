using FluentValidation;
using UserService.Application.Validators.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<User>, IUpdateUserValidator
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.FirstNames)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .When(user => user.FirstNames.Any())
                .WithMessage("First names are required.")
                .Length(2, 255)
                .When(user => user.FirstNames.Any())
                .WithMessage("First names must be between 2 and 255 characters.");
            RuleFor(user => user.LastNames)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .When(user => user.FirstNames.Any())
                .WithMessage("Last names are required.")
                .Length(2, 255)
                .When(user => user.FirstNames.Any())
                .WithMessage("Last names must be between 2 and 255 characters.");
            RuleFor(user => user.ShortName)
                .NotEmpty()
                .When(user => user.FirstNames.Any())
                .WithMessage("Short name is required.")
                .Length(2, 100)
                .When(user => user.FirstNames.Any())
                .WithMessage("Short name must be between 2 and 100 characters.");
            // RuleFor(user => user.Address)
            //     .Length(2, 255)
            //     .WithMessage("Address must be between 2 and 255 characters.");
            // RuleFor(user => user.PhoneNumber)
            //     .Matches(@"^\+?[0-9]{10,15}$")
            //     .WithMessage("Phone number must be a valid format.");
            RuleFor(user => user.Email)
                .NotEmpty()
                .When(user => user.FirstNames.Any())
                .WithMessage("Email is required.")
                .EmailAddress()
                .When(user => user.FirstNames.Any())
                .WithMessage("Email must be a valid email address.");
            RuleFor(user => user.CI)
                .NotEmpty()
                .When(user => user.FirstNames.Any())
                .WithMessage("CI is required.")
                .Length(2, 50)
                .When(user => user.FirstNames.Any())
                .WithMessage("CI must be between 2 and 50 characters.");
            // RuleFor(user => user.ImageUrl)
            //     .NotEmpty()
            //     .WithMessage("Image URL is required.")
            //     .Matches(@"^(http|https)://[^\s]+$")
            //     .WithMessage("Image URL must be a valid URL.");
        }
    }
}
