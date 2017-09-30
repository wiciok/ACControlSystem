using ACControlSystemApi.Utils.Tokens.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
