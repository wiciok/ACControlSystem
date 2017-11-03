using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Generic;

namespace ACCSApi.Repositories.Specific
{
    public class ACScheduleRepository: BaseRepository<IACSchedule>, IACScheduleRepository
    {
        public ACScheduleRepository(IDao<IACSchedule> dao): base(dao)
        {

        }
    }
}
