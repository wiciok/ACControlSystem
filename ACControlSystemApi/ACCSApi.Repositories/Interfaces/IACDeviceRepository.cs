﻿using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Generic;

namespace ACCSApi.Repositories.Interfaces
{
    public interface IACDeviceRepository: IRepository<IACDevice>
    {
        IACDevice CurrentDevice { get; set; }
    }
}
