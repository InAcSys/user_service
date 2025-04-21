using Microsoft.EntityFrameworkCore;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;
using UserService.Utils;

namespace UserService.Infrastructure.Repositories.Abstracts
{
    public abstract class UserAbstractRepository : Repository<User, Guid>, IUserRepository
    {
        protected UserAbstractRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserLogInDTO> GetByCredentials(CredentialDTO credential)
        {
            var user = await GetByEmail(credential.Email);

            if (user is null)
            {
                throw new InvalidOperationException("Credentials not found");
            }

            var password = HashPassword.PasswordGenerator(user.Salt, credential.Password);

            if (!user.Email.Equals(credential.Email) && !user.Password.Equals(password))
            {
                throw new InvalidDataException("Invalid credentials");
            }

            var logIn = new UserLogInDTO
            {
                UserId = user.Id,
                RoleId = user.RoleId
            };

            return logIn;
        }

        public async Task<User> GetByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            var entity = await base._dbContext.Set<User>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);

            if (entity == null)
            {
                throw new InvalidOperationException("User with the specified email was not found.");
            }

            return entity;
        }
    }
}