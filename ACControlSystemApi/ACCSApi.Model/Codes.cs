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

        public override string ToString()
        {
            return $"LeadingPulseDuration: {LeadingPulseDuration}, LeadingGapDuration: {LeadingGapDuration}, " +
                   $"OnePulseDuration: {OnePulseDuration}, OneGapDuration: {OneGapDuration}, " +
                   $"ZeroPulseDuration: {ZeroPulseDuration}, ZeroGapDuration: {ZeroGapDuration}," +
                   $"SendTrailingPulse: {SendTrailingPulse}";
        }
    }

    public class NecCode : ICode
    {
        public NecCode(string code)
        {
            Code = code;
        }

        public string Code { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is NecCode other))
                return false;
            return Code.Equals(other.Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }

    public class RawCode : ICode
    {
        public RawCode(int[] code)
        {
            Code = code;
        }

        public int[] Code { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is RawCode other))
                return false;
            return Code.Equals(other.Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}

