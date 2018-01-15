using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Services.Interfaces
{
    public interface IUserService
    {
        IUser GetUser(int id);
        IEnumerable<IUserPublic> GetAllUsers();
        IUser FindUser(string email);
        void RemoveUser(int id);
        void UpdateUserAuthData(IUserRegister user);
        void UpdateUserPublicData(IUserPublic userData);
        int AddUser(IUserRegister userData);   
    }
}
