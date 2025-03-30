using FluentValidation;
using UserService.Application.Services.Interfaces;

namespace UserService.Application.Services.Abstracts
{
    public class Service<T, TKey>
    (
        IValidator<T> validator
    ) : IService<T, TKey>
    {

        protected readonly IValidator<T> _validator = validator;
        public Task<T> Create(T entity)
        {
            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return Task.FromResult(true);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return Task.FromResult<T?>(default);
        }

        public Task<T?> GetByName(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            return Task.FromResult<T?>(default);
        }

        public Task<T> Update(T entity)
        {
            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            return Task.FromResult(entity);
        }
    }
}