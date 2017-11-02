using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Models.Settings;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Repositories.Specific
{
    public class ACDeviceRepository : BaseRepository<IACDevice>, IACDeviceRepository
    {
        public ACDeviceRepository(IDao<IACDevice> deviceDao) : base(deviceDao)
        {
            //CreateInitialDataTemp();
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
                    NecCodeSettings = new NecCodeSettings()
                    {
                        LeadingPulseDuration = 3200,
                        LeadingGapDuration = 1600,
                        OneGapDuration = 1200,
                        OnePulseDuration = 410,
                        ZeroPulseDuration = 410,
                        ZeroGapDuration = 410,
                        SendTrailingPulse = true
                    },                   
                    AvailableSettings= new ACSetting[]
                    {
                        new ACSetting()
                        {
                            Code=new NecCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01000000 00111111"
                            },
                            IsTurnOff=true,
                            Settings=null
                        },

                        new ACSetting()
                        {
                            Code=new NecCode()
                            {
                                Code ="00101000 11000110 00000000 00001000 00001000 01111111 10010000 00001100 10001010 10000000 00001100 00000000 00000000 00000000 00000100 01110100"
                            },
                            IsTurnOff=true,
                            Settings=null
                        },

                        new ACSetting()
                        {
                            Code=new NecCode()
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

        //todo: check if interface here is not causing any problems
        public IACDevice CurrentACDevice
        {
            get => this.Get(GlobalSettings.currentACDeviceId);
            set => GlobalSettings.currentACDeviceId = value.Id;
        }
    }
}
