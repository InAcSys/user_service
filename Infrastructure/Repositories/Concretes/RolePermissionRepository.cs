using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class RolePermissionRepository : Repository<RolePermission, int>
    {
        public RolePermissionRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}