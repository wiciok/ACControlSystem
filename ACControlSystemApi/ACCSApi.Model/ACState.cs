using System;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    public class ACState : IACState
    {
        public bool? IsTurnOff { get; set; }
        public Guid? ACSettingGuid { get; set; }
    }
}
