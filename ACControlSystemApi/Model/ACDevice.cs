using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model
{
    public class ACDevice: IACControlSystemSerializableClass
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
        
        public IList<IRCode> AvailableIRCodes { get; set; }
    }
}
