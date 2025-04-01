using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}