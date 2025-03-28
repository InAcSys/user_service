using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Context
{
    public class UserServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        // TODO: Insert DbSets
    }
}