using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Transferable
{
    public class AcSettingAdd
    {
        public bool IsTurnOff { get; set; }
        public JObject Settings { get; set; }
    }
}
