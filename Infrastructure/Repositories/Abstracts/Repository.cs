using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Infrastructure.Repositories.Abstracts
{
    public class Repository<T, TKey>(DbContext dbContext) : IRepository<T, TKey> where T : class
    {
        protected readonly DbContext _dbContext = dbContext;

        public async virtual Task<T> Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<bool> Delete(TKey id)
        {
            await _dbContext.Set<T>().FindAsync(id);
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity is null)
            {
                return false;
            }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async virtual Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var entities = await _dbContext.Set<T>()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return entities;
        }

        public async virtual Task<T?> GetById(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async virtual Task<T?> GetByName(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
            return entity;
        }

        public async virtual Task<T> Update(TKey id, T entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var existingEntity = await _dbContext.Set<T>().FindAsync(id);
            if (existingEntity is null)
            {
                throw new InvalidOperationException("The entity to update was not found.");
            }
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}