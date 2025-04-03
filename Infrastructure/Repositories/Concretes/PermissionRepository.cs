using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class PermissionRepository : Repository<Permission, int>
    {
        public PermissionRepository(DbContext context) : base(context)
        {
        }
    }
}