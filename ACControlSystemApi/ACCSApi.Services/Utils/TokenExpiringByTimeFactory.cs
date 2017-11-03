using System;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models;

namespace ACCSApi.Services.Utils
{
    public class TokenExpiringByTimeFactory : ITokenFactory
    {
        private readonly int _expirationTime = Repositories.Models.GlobalConfig.TokenExpirationTimeInSeconds;

        public IToken GenerateToken()
        {
            return new TokenExpiringByTime(DateTime.Now.AddSeconds((int)_expirationTime));
        }
    }
}
