using ACControlSystemApi.Model;
using ACControlSystemApi.Model.Interfaces;
using ACControlSystemApi.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public class UserRepository: BaseRepository<IUser>, IUserRepository
    {
        public UserRepository(IDao<IUser> dao): base(dao)
        {

        }
    }
}
