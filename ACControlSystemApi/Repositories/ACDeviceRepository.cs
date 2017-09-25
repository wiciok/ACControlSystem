using ACControlSystemApi.Model;
using ACControlSystemApi.Repositories.Generic;
using ACControlSystemApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public interface IACDeviceRepository
    {
        ACDevice CurrentACDevice { get; set; }
    }

    public class ACDeviceRepository : BaseRepository<ACDevice>, IACDeviceRepository
    {
        private ACDevice _currentDevice;

        public ACDeviceRepository(IDao<ACDevice> deviceDao) : base(deviceDao)
        {
            CreateInitialDataTemp();
        }

        public void CreateInitialDataTemp()
        {
            Add(new ACDevice[]
            {
                new ACDevice()
                {
                    Id=1,
                    Brand="Fujitsu",
                    Model="",
                    ModulationFrequencyInHz=38,
                    DutyCycle=0.5,
                    AvailableIRCodes= new IRCode[]
                    {
                        new IRCode
                        (
                            new RawCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01000000 00111111"
                            }
                        )
                        {
                            Id=1,
                            Name="turn off code",
                            Description="turn off code",
                            IsTurnOffCode=true
                        },

                        new IRCode
                        (
                            new RawCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01111111 10010000 00001100 10001010 10000000 00001100 00000000 00000000 00000000 00000100 01110100"
                            }
                        )
                        {
                            Id=1,
                            Name="wl, cool",
                            Description="wl, cool",
                            IsTurnOffCode=false
                        }
                    }
}
            });
        }

        public ACDevice CurrentACDevice
        {
            get => this.Get(GlobalSettings.currentACDeviceId);
            set => GlobalSettings.currentACDeviceId = value.Id;
        }
    }
}
