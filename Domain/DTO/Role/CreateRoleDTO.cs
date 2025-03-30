namespace UserService.Domain.DTOs.Role
{
    public class CreateRoleDTO
    {
        public string Name { get; set; } = "";
        public IEnumerable<int> PermissionIds { get; set; } = new List<int>();
    }
}