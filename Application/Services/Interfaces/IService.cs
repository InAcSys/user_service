namespace UserService.Application.Services.Interfaces
{
    public interface IService<T, TKey>
    {
        public Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize, Guid tenantId);
        public Task<T?> GetById(TKey id, Guid tenantId);
        public Task<T?> GetByName(string name, Guid tenantId);
        public Task<T> Create(T entity);
        public Task<T> Update(TKey id, T entity, Guid tenantId);
        public Task<bool> Delete(TKey id, Guid tenantId);
    }
}