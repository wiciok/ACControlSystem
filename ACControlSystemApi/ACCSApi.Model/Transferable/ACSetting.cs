using System;
using ACCSApi.Model.Interfaces;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model.Transferable
{
    public class ACSetting : IACSetting
    {
        public ACSetting()
        {
            UniqueId = Guid.NewGuid();
            IsTurnOff = false;
        }

        public Guid UniqueId { get; }
        public bool IsTurnOff { get; set; }
        public JObject Settings { get; set; }
        public ICode Code { get; set; }
    }
}
