namespace UserService.Domain.Entities.Interfaces
{
    public interface ITenant
    {
        public Guid TenantId { get; set; }
    }
}