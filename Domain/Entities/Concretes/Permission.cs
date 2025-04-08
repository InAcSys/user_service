using UserService.Domain.Entities.Abstracts;

namespace UserService.Domain.Entities.Concretes
{
    public class Permission : Entity<int>
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
    }
}