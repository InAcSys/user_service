using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class RolePermissionService : Service<RolePermission, int>
    {
        public RolePermissionService(IValidator<RolePermission> validator, IRepository<RolePermission, int> repository) : base(validator, repository)
        {
        }
    }
}