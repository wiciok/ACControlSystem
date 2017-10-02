using ACControlSystemApi.Model;
using ACControlSystemApi.Repositories.Generic;
using ACControlSystemApi.Utils;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace ACControlSystemApi.Repositories
{
    public class ACDeviceRepository : BaseRepository<IACDevice>, IACDeviceRepository
    {
        private ACDevice _currentDevice;

        public ACDeviceRepository(IDao<IACDevice> deviceDao) : base(deviceDao)
        {
            CreateInitialDataTemp();
        }

        public void CreateInitialDataTemp()
        {
            Add(new ACDevice[]
            {
                new ACDevice()
                {


                    Id =1,
                    Brand="Fujitsu",
                    Model="",
                    ModulationFrequencyInHz=38,
                    DutyCycle=0.5,
                    AvailableSettings= new ACSetting[]
                    {
                        new ACSetting()
                        {
                            Code=new RawCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01000000 00111111"
                            },
                            IsTurnOff=true,
                            Settings=null
                        },

                        new ACSetting()
                        {
                            Code=new RawCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01111111 10010000 00001100 10001010 10000000 00001100 00000000 00000000 00000000 00000100 01110100"
                            },
                            IsTurnOff=true,
                            Settings=null
                        },

                        new ACSetting()
                        {
                            Code=new RawCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01111111 10010000 00001100 10001010 10000000 00001100 00000000 00000000 00000000 00000100 01110100"
                            },
                            IsTurnOff=false,
                            Settings=new JObject()
                            {
                                { "Mode" , "Cool" }
                            }
                        }
                    }
                }
            });
        }

        public IACDevice CurrentACDevice
        {
            get => this.Get(GlobalSettings.currentACDeviceId);
            set => GlobalSettings.currentACDeviceId = value.Id;
        }
    }
}
