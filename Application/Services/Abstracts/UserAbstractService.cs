using FluentValidation;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Abstracts
{
    public abstract class UserAbstractService : Service<User, Guid>, IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IValidator<CredentialDTO> _credentialValidator;
        protected UserAbstractService(IValidator<User> validator, IValidator<CredentialDTO> credentialsValidator, IUserRepository userRepository, IRepository<User, Guid> repository) : base(validator, repository)
        {
            _userRepository = userRepository;
            _credentialValidator = credentialsValidator;
        }

        public async Task<UserLogInDTO> ValidateCredentials(CredentialDTO credential)
        {
            var result = _credentialValidator.Validate(credential);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var credentials = await _userRepository.GetByCredentials(credential);
            return credentials;
        }

        public Task<User> GetByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(email);
            }
            var user = _userRepository.GetByEmail(email);
            return user;
        }
    }
}