using System.Security.Cryptography;
using System.Text;
using ACCSApi.Services.Interfaces;

namespace ACCSApi.Services.Utils
{
    internal class PasswordHashingService : IPasswordHashingService
    {
        public string CreateHash(string password)
        {
            var hash = SHA256.Create();
            var tmp = Encoding.Unicode.GetBytes(password.ToCharArray());
            return hash.ComputeHash(tmp).ToString();
        }
    }
}
