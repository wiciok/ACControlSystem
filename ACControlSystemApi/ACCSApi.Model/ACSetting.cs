using System;
using ACCSApi.Model.Interfaces;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model
{
    public class ACSetting : IACSetting
    {
        public ACSetting(JObject settings, ICode code, bool isTurnOff = false)
        {
            UniqueId = Guid.NewGuid();
            IsTurnOff = isTurnOff;
            Settings = settings;
            Code = code;
        }

        public Guid UniqueId { get; }
        public bool IsTurnOff { get; }
        public JObject Settings { get; }
        public ICode Code { get; }
    }
}
