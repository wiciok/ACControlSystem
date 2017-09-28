using ACControlSystemApi.Model.Interfaces;
using ACControlSystemApi.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<IUser>
    {
    }
}
