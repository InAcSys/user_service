using UserService.Domain.Entities.Interfaces;

namespace UserService.Domain.Entities.Abstracts
{
    public class IntermediaryEntity : IDateStamp, ITenant
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid TenantId { get; set; }
    }
}