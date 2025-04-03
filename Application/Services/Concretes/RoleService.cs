using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class RoleService : Service<Role, int>
    {
        public RoleService(IValidator<Role> validator, IRepository<Role, int> repository) : base(validator, repository)
        {
        }
    }
}