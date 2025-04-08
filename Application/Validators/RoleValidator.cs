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
                .Length(3, 100)
                .WithMessage("Role name must be between 3 and 100 characters.");
        }
    }
}