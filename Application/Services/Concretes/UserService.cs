using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class UserSystemService : UserAbstractService
    {
        public UserSystemService(IRepository<User, Guid> repository, IValidator<User> validator, IValidator<CredentialDTO> credentialsValidator, IUserRepository userRepository) : base(validator, credentialsValidator, userRepository, repository)
        {
        }
    }
}