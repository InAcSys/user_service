using UserService.Domain.Entities.Abstracts;

namespace UserService.Domain.Entities.Concretes
{
    public class RolePermission : IntermediaryEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; } = new();
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = new();
    }
}