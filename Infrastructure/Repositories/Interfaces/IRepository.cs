namespace UserService.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize);
        Task<T?> GetById(TKey id);
        Task<T?> GetByName(string name);
        Task<T> Create(T entity);
        Task<T> Update(TKey id, T entity);
        Task<bool> Delete(TKey id);
    }
}