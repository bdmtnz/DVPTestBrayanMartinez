using DoubleVPartners.BackEnd.Domain.Common.Base;
using System.Security.Cryptography;
using System.Text;

namespace DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Credentials.ValueObjects
{
    public class UserCredentialKey : ValueObject
    {
        public string Encrypted { get; private set; }

        private UserCredentialKey(string encrypted)
        {
            Encrypted = encrypted;
        }

        public static UserCredentialKey Create(string user, string password)
        {
            HashAlgorithm hash = SHA256.Create();

            // compute hash of the password prefixing password with the salt
            byte[] plainTextBytes = Encoding.UTF8.GetBytes($"{user.Trim().ToLower()}~{password.Trim()}");
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            string hashValue = Convert.ToBase64String(hashBytes);
            return new UserCredentialKey(hashValue);
        }

        public static UserCredentialKey Create(string encrypted)
        {
            return new UserCredentialKey(encrypted);
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return this;
        }
    }
}
