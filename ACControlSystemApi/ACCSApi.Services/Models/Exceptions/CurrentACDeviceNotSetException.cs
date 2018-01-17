using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Services.Models.Exceptions
{
    public class CurrentACDeviceNotSetException : Exception
    {
        public CurrentACDeviceNotSetException(string message = "Current ACDevice not set!")
        { }
    }
}
