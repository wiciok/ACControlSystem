using ACControlSystemApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IACDeviceRepository
    {
        ACDevice CurrentACDevice { get; set; }
    }
}
