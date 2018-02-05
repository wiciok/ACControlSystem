using System;

namespace ACCSApi.Services.Models.Exceptions
{
    public class ACStateUndefinedException : Exception
    {
        public ACStateUndefinedException(string message = "Host device doesn't control AC Device state at the moment") : base(message)
        { }
    }
}
