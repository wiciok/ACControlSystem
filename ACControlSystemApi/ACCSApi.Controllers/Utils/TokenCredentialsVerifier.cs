using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Services.Interfaces;
using Bazinga.AspNetCore.Authentication.Basic;

namespace ACCSApi.Controllers.Utils
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
            //todo: restore this
            return Task.FromResult(_authService.CheckAuthentication(password));
           // return Task.FromResult(true);
        }
    }
}
