using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class RoleRepository : Repository<Role, int>
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}