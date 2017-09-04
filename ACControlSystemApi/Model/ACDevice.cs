using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Model
{
    public class ACDevice
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        
        // ir transmission related properties:

        public int ModulationFrequencyInHz { get; set; }
        public int DutyCycle { get; set; }
        
        public List<ICode> AvailableCodes { get; set; }
        //here should also be all codes to send
    }
}
