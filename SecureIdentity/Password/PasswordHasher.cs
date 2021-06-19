using System;
using System.Linq;
using System.Security.Cryptography;
using SecureIdentity.Password.Exceptions;

namespace SecureIdentity.Password
{
    public static class PasswordHasher
    {
        public static string Hash(
            string password,
            short saltSize = 16,
            short keySize = 32,
            int iterations = 10000,
            char splitChar = '.')
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidPasswordException("Password should not be null or empty");

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{iterations}{splitChar}{salt}{splitChar}{key}";
        }

        public static bool Verify(
            string hash,
            string password,
            short keySize = 32,
            int iterations = 10000,
            char splitChar = '.')
        {
            var parts = hash.Split(splitChar, 3);
            if (parts.Length != 3)
                return false;

            var hashIterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            if (hashIterations != iterations)
                return false;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(keySize);

            return keyToCheck.SequenceEqual(key);
        }
    }
}