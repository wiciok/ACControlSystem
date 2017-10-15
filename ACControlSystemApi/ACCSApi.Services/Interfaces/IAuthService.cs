using ACCSApi.Model.Transferable;

namespace ACCSApi.Services.Interfaces
{
    public interface IAuthService
    {
        bool CheckAuthentication(string tokenString);
        string TryAuthenticate(AuthPackage auth);
    }
}
