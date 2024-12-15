using System;
using System.Security.Cryptography;
using System.Text;

namespace CryII.EasyHelper
{
    public static class HashHelper
    {
        public static string Code(int length)
        {
            var algorithmName = HashAlgorithmName.SHA256.Name;
            var hashAlgorithm = HashAlgorithm.Create(algorithmName);

            var randomBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            if (hashAlgorithm == null)
            {
                throw new NullReferenceException("hashAlgorithm is null");
            }

            var hashBytes = hashAlgorithm.ComputeHash(randomBytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            var hash = sb.ToString();
            return hash[..length];
        }
    }
}