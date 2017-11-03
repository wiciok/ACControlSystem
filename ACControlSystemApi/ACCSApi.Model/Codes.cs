using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model
{
    public class NecCodeSettings
    {
        public NecCodeSettings
        (           
            int leadingPulseDuration,
            int leadingGapDuration,
            int onePulseDuration,
            int zeroPulseDuration,
            int oneGapDuration,
            int zeroGapDuration,
            bool sendTrailingPulse)
        {
            LeadingPulseDuration = leadingPulseDuration;
            LeadingGapDuration = leadingGapDuration;
            OnePulseDuration = onePulseDuration;
            ZeroPulseDuration = zeroPulseDuration;
            OneGapDuration = oneGapDuration;
            ZeroGapDuration = zeroGapDuration;
            SendTrailingPulse = sendTrailingPulse;
        }

        public int LeadingPulseDuration { get; }
        public int LeadingGapDuration { get; }
        public int OnePulseDuration { get; }
        public int ZeroPulseDuration { get; }
        public int OneGapDuration { get; }
        public int ZeroGapDuration { get;}
        public bool SendTrailingPulse { get; }
    }

    public class NecCode : ICode
    {
        public string Code { get; set; }
    }

    public class RawCode : ICode
    {
        public int[] Code { get; set; }
    }
}

