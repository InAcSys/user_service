using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class PermissionService : Service<Permission, int>
    {
        public PermissionService(IValidator<Permission> validator, IRepository<Permission, int> repository) : base(validator, repository)
        {
        }
    }
}