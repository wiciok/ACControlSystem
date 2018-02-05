using System;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Utils;

namespace ACCSApi.Services.Models
{
    public class TokenExpiringByTime : IToken
    {
        public TokenExpiringByTime(DateTime expirationDateTime)
        {
            TokenString = UniqueStringGenerator.GenerateUniqueToken();
            CreationDate = DateTime.Now;
            ExpirationTime = expirationDateTime;
        }

        public string TokenString { get; }
        public bool IsExpired => DateTime.Now >= ExpirationTime;

        public DateTime CreationDate { get; }
        public DateTime ExpirationTime { get; }
    }
}
