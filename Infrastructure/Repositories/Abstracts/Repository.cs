using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Infrastructure.Repositories.Abstracts
{
    public class Repository<T, TKey>(UserServiceDbContext dbContext) : IRepository<T, TKey> where T : class
    {
        protected readonly UserServiceDbContext _dbContext = dbContext;

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

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
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

        public async virtual Task<T> Update(T entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}