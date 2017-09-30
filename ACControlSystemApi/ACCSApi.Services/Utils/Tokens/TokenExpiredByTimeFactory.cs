using ACControlSystemApi.Utils.Tokens.Interfaces;
using System;

namespace ACControlSystemApi.Utils.Tokens
{
    public class TokenExpiredByTimeFactory : ITokenFactory
    {
        private int _expirationTime = GlobalSettings.TokenExpirationTimeInSeconds;

        public IToken GenerateToken()
        {
            return new TokenExpirationByTime(DateTime.Now.AddSeconds((int)_expirationTime));
        }
    }
}
