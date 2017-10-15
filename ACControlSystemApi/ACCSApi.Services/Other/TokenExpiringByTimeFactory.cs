using System;
using ACCSApi.Repositories.Models.Settings;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models;

namespace ACCSApi.Services.Other
{
    public class TokenExpiringByTimeFactory : ITokenFactory
    {
        private int _expirationTime = GlobalSettings.TokenExpirationTimeInSeconds;

        public IToken GenerateToken()
        {
            return new TokenExpiringByTime(DateTime.Now.AddSeconds((int)_expirationTime));
        }
    }
}
