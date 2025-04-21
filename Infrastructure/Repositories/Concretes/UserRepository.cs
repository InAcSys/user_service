using UserService.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class UserRepository : UserAbstractRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<User> Create(User entity)
        {
            var salt = HashPassword.SaltGenerator();
            var encryptedPassword = HashPassword.PasswordGenerator(salt, entity.Password);
            entity.Password = encryptedPassword;
            entity.Salt = salt;
            return base.Create(entity);
        }
    }
}