using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;

namespace UserService.Infrastructure.Context
{
    public class UserServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
    }
}