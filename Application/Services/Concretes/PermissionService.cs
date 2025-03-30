using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.DTOs.Permission;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class PermissionService : Service<PermissionDTO, int>
    {
        public PermissionService(IValidator<PermissionDTO> validator, IRepository<PermissionDTO, int> repository) : base(validator, repository)
        {
        }
    }
}