using FluentValidation;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(role => role.Name)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .Length(3, 50)
                .WithMessage("Role name must be between 3 and 50 characters.");
            
            RuleFor(role => role.RolePermissions)
                .NotNull()
                .WithMessage("Permission IDs are required.")
                .Must(rolePermissions => rolePermissions.Any())
                .WithMessage("At least one permission ID is required.")
                .Must(rolePermissions => rolePermissions.All(rp => rp.Permission.Id > 0))
                .WithMessage("All permission IDs must be positive integers.");
        }
    }
}