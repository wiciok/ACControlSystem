using ACCSApi.Model.Interfaces;
using System;

namespace ACControlSystemApi.Model
{
    public class IRCode : IACCSSerializable
    {
        private ICode _code;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICode Code { get => _code; }

        public bool IsTurnOffCode { get; set; }

        public object SettingsArray { get => new NotImplementedException(); }
        //todo: add fields describing different settings like temperature, humidity, mode, etc.
        // best way to do it is via using some kind of dynamic dictionary (could be passed from frontend as json)

        public IRCode(ICode code, bool isTurnOffCode=false)
        {
            _code = code;
            IsTurnOffCode = isTurnOffCode;
        }
    }

    public class NecCode : ICode
    {
        public int LeadingPulseDuration { get; set; }
        public int LeadingGapDuration { get; set; }
        public int OnePulseDuration { get; set; }
        public int ZeroPulseDuration { get; set; }
        public int OneGapDuration { get; set; }
        public int ZeroGapDuration { get; set; }
        public bool SendTrailingPulse { get; set; }

        public string Code { get; set; }
    }

    public class RawCode : ICode
    {
        public string Code { get; set; }
    }
}

