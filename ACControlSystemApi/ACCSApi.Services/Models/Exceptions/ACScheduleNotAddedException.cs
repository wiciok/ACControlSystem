using System;

namespace ACCSApi.Services.Models.Exceptions
{
    public class ACScheduleNotAddedException: Exception
    {
        public ACScheduleNotAddedException(string message = "ACSchedule not created due to unspecified error") : base(message)
        { }        
    }
}
