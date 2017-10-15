using ACCSApi.Model.Transferable;

namespace ACCSApi.Model.Interfaces
{
    public interface IUserRegister
    {
        AuthPackage AuthenticationData { get; set; }
    }
}
