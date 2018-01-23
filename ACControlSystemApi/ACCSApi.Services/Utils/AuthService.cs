using System.Collections.Concurrent;
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
        private static ConcurrentDictionary<IUser, IToken> _tokensDictionary;

        public AuthService(IUserService userService, ITokenFactory tokenFactory)
        {
            _userService = userService;
            _tokenFactory = tokenFactory;

            if (_tokensDictionary == null)
                _tokensDictionary = new ConcurrentDictionary<IUser, IToken>();
        }

        public bool CheckAuthentication(string tokenString)
        {
            var tokenDictRecord = _tokensDictionary.SingleOrDefault(x => x.Value.TokenString.Equals(tokenString));
            if (tokenDictRecord.Value?.TokenString == null)
                return false;
            if (tokenDictRecord.Value.IsExpired)
            {
                _tokensDictionary.TryRemove(tokenDictRecord.Key, out var value);
                return false;
            }
            else
                return true;
        }

        public string TryAuthenticate(AuthData auth)
        {
            var user = _userService.FindUser(auth.EmailAddress);

            if (user == null)
                throw new ItemNotFoundException("User with specified email address doesn't exist!");

            if (!auth.PasswordHash.Equals(user.PasswordHash))
                return null; //unauthorized

            _tokensDictionary.TryGetValue(user, out var token);

            if (token == null)
                return CreateTokenOrReturnExisting(user);

            if (!token.IsExpired)
                return token.TokenString;

            _tokensDictionary.TryRemove(user, out var value);
            return CreateTokenOrReturnExisting(user);
        }

        private string CreateTokenOrReturnExisting(IUser user)
        {
            var token = _tokenFactory.GenerateToken();
            if (_tokensDictionary.TryAdd(user, token))
                return token.TokenString;
            _tokensDictionary.TryGetValue(user, out var tokenFromOtherThread);
            return tokenFromOtherThread.TokenString; //if some weird worst-scenario concurrent things occures -> return null (unauthorized)
        }
    }
}
