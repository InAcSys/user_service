namespace UserService.Infrastructure.Repositories.Concretes
{
    using UserService.Domain.DTOs.Role;
    using UserService.Infrastructure.Context;
    using UserService.Infrastructure.Repositories.Abstracts;

    public class RoleRepository : Repository<RoleDTO, int>
    {
        public RoleRepository(UserServiceDbContext context) : base(context)
        {
        }
    }
}