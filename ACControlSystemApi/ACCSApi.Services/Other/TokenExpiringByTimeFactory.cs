using ACControlSystemApi.Utils.Tokens.Interfaces;
using System;

namespace ACControlSystemApi.Utils.Tokens
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
