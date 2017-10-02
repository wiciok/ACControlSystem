using ACCSApi.Model.Interfaces;

namespace ACControlSystemApi.Model
{
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

