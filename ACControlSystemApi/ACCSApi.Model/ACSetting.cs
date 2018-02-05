using System;
using ACCSApi.Model.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Model
{
    public class ACSetting : IACSetting
    {
        [JsonConstructor]
        public ACSetting(JObject settings, ICode code, Guid uniqueId, bool isTurnOff = false)
        {
            IsTurnOff = isTurnOff;
            Settings = settings;
            Code = code;
            UniqueId = uniqueId;
        }

        public ACSetting(JObject settings, ICode code, bool isTurnOff = false) : this(settings, code, Guid.NewGuid(), isTurnOff)
        {
        }

        public Guid UniqueId { get; }
        public bool IsTurnOff { get; }
        public JObject Settings { get; }
        public ICode Code { get; }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode() + IsTurnOff.GetHashCode() + Settings.GetHashCode() + Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ACSetting other))
                return false;
            return UniqueId.Equals(other.UniqueId)
                   && IsTurnOff.Equals(other.IsTurnOff)
                   && JToken.DeepEquals(Settings, other.Settings)
                   && Code.Equals(other.Code);
        }
    }
}
