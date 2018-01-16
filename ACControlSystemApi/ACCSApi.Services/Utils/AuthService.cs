using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Utils
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenFactory _tokenFactory;
        private static IDictionary<IUser, IToken> _tokensDictionary;

        public AuthService(IUserService userService, ITokenFactory tokenFactory)
        {
            _userService = userService;
            _tokenFactory = tokenFactory;

            if (_tokensDictionary == null)
                _tokensDictionary = new Dictionary<IUser, IToken>();
        }

        public bool CheckAuthentication(string tokenString)
        {
            var tokenDictRecord = _tokensDictionary.SingleOrDefault(x => x.Value.TokenString.Equals(tokenString));
            if (tokenDictRecord.Value?.TokenString == null)
                return false;
            if (!tokenDictRecord.Value.IsExpired)
                return true;
            _tokensDictionary.Remove(tokenDictRecord.Key);
            return false;
        }

        public string TryAuthenticate(AuthData auth)
        {
            var user = _userService.FindUser(auth.EmailAddress);

            if (user == null)
                throw new ItemNotFoundException("User with specified email address doesn't exist!");

            if (_tokensDictionary.SingleOrDefault(x => x.Key.Equals(user)).Key != null)
                _tokensDictionary.Remove(user);

            return auth.PasswordHash.Equals(user.PasswordHash) ? CreateToken(user) : null;
        }

        private string CreateToken(IUser user)
        {
            var token = _tokenFactory.GenerateToken();
            _tokensDictionary.Add(user, token);
            return token.TokenString;
        }
    }
}
