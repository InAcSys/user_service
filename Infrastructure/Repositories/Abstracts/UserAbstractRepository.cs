using Microsoft.EntityFrameworkCore;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;
using UserService.Utils;

namespace UserService.Infrastructure.Repositories.Abstracts
{
    public abstract class UserAbstractRepository(DbContext dbContext)
        : Repository<User, Guid>(dbContext),
            IUserRepository
    {
        public async Task<int> Count(Guid tenantId)
        {
            var size = await _context
                .Set<User>()
                .Where(user => user.TenantId == tenantId && user.IsActive)
                .CountAsync();
            return size;
        }

        public async Task<UserLogInDTO> GetByCredentials(CredentialDTO credential)
        {
            var user = await GetByEmail(credential.Email);

            if (user is null)
            {
                throw new InvalidOperationException("Credentials not found");
            }

            var password = HashPassword.PasswordGenerator(user.Salt, credential.Password);

            if (!user.Email.Equals(credential.Email) || !user.Password.Equals(password))
            {
                throw new InvalidDataException("Invalid credentials");
            }

            var logIn = new UserLogInDTO
            {
                UserId = user.Id,
                RoleId = user.RoleId,
                TenantId = user.TenantId,
            };

            return logIn;
        }

        public async Task<User> GetByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            var entity = await _context
                .Set<User>()
                .FirstOrDefaultAsync(e => e.Email == email && e.IsActive);

            if (entity == null)
            {
                throw new InvalidOperationException("User with the specified email was not found.");
            }

            return entity;
        }

        public async Task<IEnumerable<User>> Search(
            int pageNumber,
            int pageSize,
            Guid tenantId,
            string search
        )
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    "Page number must be greater than or equal to 1."
                );
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    "Page size must be greater than or equal to 1."
                );
            }

            var skip = (pageNumber - 1) * pageSize;

            var query = _context.Set<User>().Where(x => x.IsActive);

            if (tenantId != Guid.Empty)
            {
                query = query.Where(x => x.TenantId == tenantId || x.TenantId == Guid.Empty);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(x =>
                    (x.LastNames + " " + x.FirstNames).ToLower().Contains(search)
                );
            }

            var entities = await query.Skip(skip).Take(pageSize).ToListAsync();

            return entities;
        }

        public async Task<int> CountSearchResults(string search, Guid tenantId)
        {
            if (string.IsNullOrWhiteSpace(search))
                return 0;

            search = search.ToLower();

            var query = _context.Set<User>().Where(user => user.IsActive);

            if (tenantId != Guid.Empty)
            {
                query = query.Where(user =>
                    user.TenantId == tenantId || user.TenantId == Guid.Empty
                );
            }

            return await query
                .Where(user => (user.LastNames + " " + user.FirstNames).ToLower().Contains(search))
                .CountAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersByRole(Guid tenantId, int roleId)
        {
            var users = await _context
                .Set<User>()
                .Where(user => user.TenantId == tenantId && user.RoleId == roleId && user.IsActive)
                .ToListAsync();

            return users;
        }
    }
}
