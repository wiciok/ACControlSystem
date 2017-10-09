using ACControlSystemApi.Repositories.Generic;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Repositories.Specific
{
    public class ACScheduleRepository: BaseRepository<IACSchedule>, IACScheduleRepository
    {
        public ACScheduleRepository(IDao<IACSchedule> dao): base(dao)
        {

        }
    }
}
