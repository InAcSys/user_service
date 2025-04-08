using UserService.Domain.Entities.Abstracts;

namespace UserService.Domain.Entities.Concretes
{
    public class RolePermission : Entity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}