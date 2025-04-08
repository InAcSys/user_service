using UserService.Domain.Entities.Abstracts;

namespace UserService.Domain.Entities.Concretes
{
    public class Role : Entity<int>
    {
        public string Name { get; set; } = "";
    }
}