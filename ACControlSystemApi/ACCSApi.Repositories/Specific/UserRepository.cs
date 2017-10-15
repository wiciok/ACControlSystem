using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;

namespace ACCSApi.Repositories.Specific
{
    public class UserRepository: BaseRepository<IUser>, IUserRepository
    {
        public UserRepository(IDao<IUser> dao): base(dao)
        {

        }
    }
}
