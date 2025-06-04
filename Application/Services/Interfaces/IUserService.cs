using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Services.Interfaces
{
    public interface IUserService : IService<User, Guid>
    {
        Task<int> Count(Guid tenantId);
        Task<User> GetByEmail(string email);
        Task<UserLogInDTO> ValidateCredentials(CredentialDTO credential);
        Task<IEnumerable<User>> Search(int pageNumber, int pageSize, Guid tenantId, string search);
        Task<int> CountSearchResults(string search, Guid tenantId);
    }
}
