using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Services.Models.Exceptions
{
    public class ACStateUndefinedException : Exception
    {
        public ACStateUndefinedException(string message = "Host device doesn't control AC Device state at the moment") : base(message)
        {

        }
    }
}
