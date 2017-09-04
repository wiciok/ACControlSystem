
namespace ACControlSystemApi.Model
{
    public interface ICode
    {
        int Id { get; }
    }

    //todo: add fields describing different settings like temperature, humidity, mode, etc.

    public class NecCode: ICode
    {
        public int Id { get; set; }

        public int LeadingPulseDuration { get; set; }
        public int LeadingGapDuration { get; set; }
        public int OnePulseDuration { get; set; }
        public int ZeroPulseDuration { get; set; }
        public int OneGapDuration { get; set; }
        public int ZeroGapDuration { get; set; }
        public bool SendTrailingPulse { get; set; }

        public byte[] Code { get; set; }
    }

    public class RawCode: ICode
    {
        public int Id { get; set; }

        public int[] Code { get; set; }
    }
}

