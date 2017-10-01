using ACCSApi.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace ACControlSystemApi.Model
{
    public class ACDevice: IACDevice, IACCSSerializable
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Brand { get; set; }
        
        // ir transmission related properties:
        public int ModulationFrequencyInHz { get; set; }
        public double DutyCycle { get; set; }

        //todo: add fields describing different settings like temperature, humidity, mode, etc.
        // best way to do it is via using some kind of dynamic dictionary (could be passed from frontend as json)
        public object AvailableSettings { get => throw new NotImplementedException(); }
        
        public IList<IIRCode> AvailableIRCodes { get; set; }
    }
}
