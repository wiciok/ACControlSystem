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
        bool RemoveUser(int id);
        bool UpdateUser(IUser user);
        bool AddUser(IUser user);
    }
}
