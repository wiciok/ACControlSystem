using ACCSApi.Model.Dto;

namespace ACCSApi.Services.Interfaces
{
    public interface IAuthService
    {
        bool CheckAuthentication(string tokenString);
        string TryAuthenticate(AuthPackage auth);
    }
}
