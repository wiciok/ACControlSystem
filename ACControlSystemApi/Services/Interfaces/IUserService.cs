using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services.Interfaces
{
    public interface IUserService
    {
        IUser GetUser(int id);
        void RemoveUser(int id);
        void UpdateUser(IUser user);
        IUser AddUser(IUser user);   
    }
}
