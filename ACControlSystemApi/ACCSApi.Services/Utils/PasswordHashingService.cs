﻿using ACControlSystemApi.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ACControlSystemApi.Services
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
