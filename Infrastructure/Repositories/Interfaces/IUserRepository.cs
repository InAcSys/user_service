using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetByEmail(string email);
        Task<UserLogInDTO> GetByCredentials(CredentialDTO credential);
    }
}