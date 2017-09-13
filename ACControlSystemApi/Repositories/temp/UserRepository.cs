using ACControlSystemApi.Model;
using ACControlSystemApi.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public class UserRepository: BaseRepository<User>, IRepository<User>
    {
        public UserRepository(IDao<User> dao): base(dao)
        {

        }
    }
}
