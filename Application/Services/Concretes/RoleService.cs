using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.DTOs.Role;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class RoleService : Service<RoleDTO, int>
    {
        public RoleService(IValidator<RoleDTO> validator, IRepository<RoleDTO, int> repository) : base(validator, repository)
        {
        }
    }
}