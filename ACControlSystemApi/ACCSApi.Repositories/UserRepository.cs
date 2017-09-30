using ACControlSystemApi.Model.Interfaces;
using ACControlSystemApi.Repositories.Generic;
using ACControlSystemApi.Repositories.Interfaces;

namespace ACControlSystemApi.Repositories
{
    public class UserRepository: BaseRepository<IUser>, IUserRepository
    {
        public UserRepository(IDao<IUser> dao): base(dao)
        {

        }
    }
}
