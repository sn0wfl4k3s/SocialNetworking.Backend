using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Security
{
    public class CryptService : ICryptService
    {
        public string CreateHash(string text)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.ASCII.GetBytes(text);

            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public async Task<string> CreateHashAsync(string text)
        {
            var hash = CreateHash(text);

            return await Task.FromResult(hash);
        }
    }
}
