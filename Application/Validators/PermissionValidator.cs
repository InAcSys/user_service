using FluentValidation;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators
{
    public class PermissionValidator : AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(permission => permission.Name)
                .NotEmpty()
                .WithMessage("Permission name is required.")
                .Length(3, 100)
                .WithMessage("Permission name must be between 3 and 100 characters.");
            RuleFor(permission => permission.Description)
                .NotEmpty()
                .WithMessage("Permission description is required.")
                .Length(10, 255)
                .WithMessage("Permission description must be between 10 and 255 characters.");
        }
    }
}