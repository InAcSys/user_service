using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace UserService.Utils
{
    public static class HashPassword
    {
        public static byte[] SaltGenerator()
        {
            return RandomNumberGenerator.GetBytes(128/8);
        }

        public static string PasswordGenerator(byte[] salt, string password)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password,
                    salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256/8
                )
            );
        }
    }
}