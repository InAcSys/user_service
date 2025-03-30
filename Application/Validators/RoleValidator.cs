using FluentValidation;
using UserService.Domain.DTOs.Role;

namespace UserService.Application.Validators
{
    public class RoleValidator : AbstractValidator<RoleDTO>
    {
        public RoleValidator()
        {
            RuleFor(role => role.Name)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .Length(3, 50)
                .WithMessage("Role name must be between 3 and 50 characters.");
            
            RuleFor(role => role.PermissionIds)
                .NotNull()
                .WithMessage("Permission IDs are required.")
                .Must(permissionIds => permissionIds.Count() > 0)
                .WithMessage("At least one permission ID is required.")
                .Must(permissionIds => permissionIds.All(id => id > 0))
                .WithMessage("All permission IDs must be positive integers.");
        }
    }
}