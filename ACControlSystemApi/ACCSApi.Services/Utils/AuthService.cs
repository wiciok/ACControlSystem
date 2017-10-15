using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;

namespace ACCSApi.Services.Utils
{
    //todo: mark as singleton in DI configuration!

    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenFactory _tokenFactory;
        private IPasswordHashingService _passHashService;
        private static IDictionary<IUser, IToken> _tokensDictionary;

        public AuthService(IUserService userService, ITokenFactory tokenFactory, IPasswordHashingService passHashService)
        {
            _userService = userService;
            _tokenFactory = tokenFactory;
            _passHashService = passHashService;

            if (_tokensDictionary == null)
                _tokensDictionary = new Dictionary<IUser, IToken>();
        }

        public bool CheckAuthentication(string tokenString)
        {
            var tokenDictRecord = _tokensDictionary.SingleOrDefault(x => x.Value.TokenString.Equals(tokenString));
            if (tokenDictRecord.Value.TokenString == null)
                return false;
            if (tokenDictRecord.Value.IsExpired)
            {
                _tokensDictionary.Remove(tokenDictRecord.Key);
                return false;
            }
            return true;
        }

        public string TryAuthenticate(AuthPackage auth)
        {
            var user = _userService.FindUser(auth.EmailAddress);

            if (user == null)
                throw new ItemNotFoundException("User with specified email address doesn't exist!");

            if (_tokensDictionary.SingleOrDefault(x => x.Key.Equals(user)).Key != null)
                _tokensDictionary.Remove(user);

            return _passHashService.CreateHash(auth.Password).Equals(user.PasswordHash) ? CreateToken(user) : null;
        }

        private string CreateToken(IUser user)
        {
            var token = _tokenFactory.GenerateToken();
            _tokensDictionary.Add(user, token);
            return token.TokenString;
        }
    }
}
