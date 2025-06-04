using FluentValidation;
using UserService.Application.Services.Abstracts;
using UserService.Application.Validators.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Concretes
{
    public class UserSystemService : UserAbstractService
    {
        public UserSystemService(
            IRepository<User, Guid> repository,
            ICreateValidator<User> createValidator,
            IUpdateValidator<User> updateValidator,
            IValidator<CredentialDTO> credentialsValidator,
            IUserRepository userRepository
        )
            : base(
                createValidator,
                updateValidator,
                credentialsValidator,
                userRepository,
                repository
            ) { }
    }
}
