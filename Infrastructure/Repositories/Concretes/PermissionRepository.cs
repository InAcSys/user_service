using UserService.Domain.DTOs.Permission;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class PermissionRepository : Repository<PermissionDTO, int>
    {
        public PermissionRepository(UserServiceDbContext context) : base(context)
        {
        }
    }
}