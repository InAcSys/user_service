using FluentValidation;
using UserService.Application.Services.Interfaces;
using UserService.Application.Validators.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

namespace UserService.Application.Services.Abstracts
{
    public abstract class UserAbstractService(
        ICreateValidator<User> createValidator,
        IUpdateValidator<User> updateValidator,
        IValidator<CredentialDTO> credentialsValidator,
        IUserRepository userRepository,
        IRepository<User, Guid> repository
    ) : Service<User, Guid>(createValidator, updateValidator, repository), IUserService
    {
        protected readonly IUserRepository _userRepository = userRepository;
        protected readonly IValidator<CredentialDTO> _credentialValidator = credentialsValidator;

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

        public Task<int> Count(Guid tenantId)
        {
            return _userRepository.Count(tenantId);
        }

        public async Task<IEnumerable<User>> Search(
            int pageNumber,
            int pageSize,
            Guid tenantId,
            string search
        )
        {
            return await _userRepository.Search(pageNumber, pageSize, tenantId, search);
        }

        public Task<int> CountSearchResults(string search, Guid tenantId)
        {
            return _userRepository.CountSearchResults(search, tenantId);
        }

        public async Task<IEnumerable<User>> GetAllUsersByRole(Guid tenantId, int roleId)
        {
            return await _userRepository.GetAllUsersByRole(tenantId, roleId);
        }
    }
}
