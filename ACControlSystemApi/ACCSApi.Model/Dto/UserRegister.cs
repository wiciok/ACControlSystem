using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Dto
{
    public class UserRegister: IUserRegister
    {
        public AuthPackage AuthenticationData { get; set; }
    }
}
