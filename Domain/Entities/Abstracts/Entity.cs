using UserService.Domain.Entities.Interfaces;

namespace UserService.Domain.Entities.Abstracts
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey? Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid TenantId { get; set; }
    }
}