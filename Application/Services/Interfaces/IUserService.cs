using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Services.Interfaces
{
    public interface IUserService : IService<User, Guid>
    {
        Task<User> GetByEmail(string email);
        Task<UserLogInDTO> ValidateCredentials(CredentialDTO credential);
    }
}