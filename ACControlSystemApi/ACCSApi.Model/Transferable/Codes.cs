using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Transferable
{
    public class NecCodeSettings
    {
        public int ModulationFrequencyInHz { get; set; }
        public double DutyCycle { get; set; } = 0.5;

        public int LeadingPulseDuration { get; set; }
        public int LeadingGapDuration { get; set; }
        public int OnePulseDuration { get; set; }
        public int ZeroPulseDuration { get; set; }
        public int OneGapDuration { get; set; }
        public int ZeroGapDuration { get; set; }
        public bool SendTrailingPulse { get; set; }
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

