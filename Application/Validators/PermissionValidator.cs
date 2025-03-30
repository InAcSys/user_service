using FluentValidation;
using UserService.Domain.DTOs.Permission;

namespace UserService.Application.Validators
{
    public class PermissionValidator : AbstractValidator<PermissionDTO>
    {
        public PermissionValidator()
        {
            RuleFor(permission => permission.Name)
                .NotEmpty()
                .WithMessage("Permission name is required.")
                .Length(3, 50)
                .WithMessage("Permission name must be between 3 and 50 characters.");
            RuleFor(permission => permission.Description)
                .NotEmpty()
                .WithMessage("Permission description is required.")
                .Length(10, 200)
                .WithMessage("Permission description must be between 10 and 200 characters.");
        }
    }
}