using FluentValidation;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators
{
    public class RolePermissionValidator : AbstractValidator<RolePermission>
    {
        public RolePermissionValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Role ID cannot be empty.");

            RuleFor(x => x.PermissionId)
                .NotEmpty()
                .WithMessage("Permission ID cannot be empty.");
        }
    }
}