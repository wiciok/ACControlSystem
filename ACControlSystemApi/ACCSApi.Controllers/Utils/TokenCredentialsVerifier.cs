using System.Threading.Tasks;
using ACCSApi.Services.Interfaces;
using Bazinga.AspNetCore.Authentication.Basic;

namespace ACCSApi.Api.Utils
{
    public class TokenCredentialsVerifier : IBasicCredentialVerifier
    {
        private readonly IAuthService _authService;

        public TokenCredentialsVerifier(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<bool> Authenticate(string username, string password)
        {
            return Task.FromResult(_authService.CheckAuthentication(password));
        }
    }
}
