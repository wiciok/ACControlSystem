using System;
using System.Collections.Generic;
using System.Text;

namespace ACCSApi.Model.Interfaces
{
    public interface IACState
    {
        bool? IsOn { get; set; }
        object Settings { get; set; }
    }
}
