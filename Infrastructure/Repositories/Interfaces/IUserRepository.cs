using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<int> Count(Guid tenantId);
        Task<User> GetByEmail(string email);
        Task<UserLogInDTO> GetByCredentials(CredentialDTO credential);
        Task<IEnumerable<User>> Search(int pageNumber, int pageSize, Guid tenantId, string search);
        Task<int> CountSearchResults(string search, Guid tenantId);
    }
}
