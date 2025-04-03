using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Repositories.Abstracts;

namespace UserService.Infrastructure.Repositories.Concretes
{
    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<User> Create(User entity)
        {
            var salt = RandomNumberGenerator.GetBytes(128/8);
            var encryptedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: entity.Password,
                    salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256/8
                )
            );
            entity.Password = encryptedPassword;
            return base.Create(entity);
        }
    }
}