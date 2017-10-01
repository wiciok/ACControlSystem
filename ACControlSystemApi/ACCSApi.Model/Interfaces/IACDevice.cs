using System.Collections.Generic;


namespace ACCSApi.Model.Interfaces
{
    public interface IACDevice: IACCSSerializable
    {
        string Model { get; set; }
        string Brand { get; set; }

        // ir transmission related properties:
        int ModulationFrequencyInHz { get; set; }
        double DutyCycle { get; set; }

        //todo: add fields describing different settings like temperature, humidity, mode, etc.
        // best way to do it is via using some kind of dynamic dictionary (could be passed from frontend as json)
        object AvailableSettings { get; }

        IList<IIRCode> AvailableIRCodes { get; set; }
    }
}
