namespace UserService.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(TKey id);
        Task<T?> GetByName(string name);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(TKey id);
    }
}