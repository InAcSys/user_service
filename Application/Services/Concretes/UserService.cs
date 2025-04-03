using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class UserSystemService : Service<User, Guid>
    {
        public UserSystemService(IRepository<User, Guid> repository, IValidator<User> validator) : base(validator, repository)
        {
        }
    }
}