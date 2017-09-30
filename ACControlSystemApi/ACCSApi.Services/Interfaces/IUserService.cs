using ACControlSystemApi.Model.Interfaces;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IUserService
    {
        IUser GetUser(int id);
        IUser FindUser(string email);
        void RemoveUser(int id);
        void UpdateUserAuthData(IUserRegister user);
        void UpdateUserPublicData(IUserPublic userData);
        int AddUser(IUserRegister userData);   
    }
}
