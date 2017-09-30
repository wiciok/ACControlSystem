using ACControlSystemApi.Utils.Tokens.Interfaces;
using System;

namespace ACControlSystemApi.Utils.Tokens
{
    public class TokenExpirationByTime : IToken
    {
        public TokenExpirationByTime(DateTime expirationDateTime)
        {
            TokenString = UniqueStringGenerator.GenerateUniqueToken();
            CreationDate = DateTime.Now;
            ExpirationTime = expirationDateTime;
        }

        public string TokenString { get; }
        public bool IsExpired
        {
            get
            {
                return DateTime.Now >= ExpirationTime;
            }
        }

        public DateTime CreationDate { get; }
        public DateTime ExpirationTime { get; }
    }
}
