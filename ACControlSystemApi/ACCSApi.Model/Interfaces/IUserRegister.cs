using ACCSApi.Model.Dto;

namespace ACCSApi.Model.Interfaces
{
    public interface IUserRegister
    {
        AuthPackage AuthenticationData { get; set; }
    }
}
