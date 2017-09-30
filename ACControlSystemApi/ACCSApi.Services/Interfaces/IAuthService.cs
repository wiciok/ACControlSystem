using ACControlSystemApi.Model;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IAuthService
    {
        bool CheckAuthentication(string tokenString);
        string TryAuthenticate(AuthPackage auth);
    }
}
