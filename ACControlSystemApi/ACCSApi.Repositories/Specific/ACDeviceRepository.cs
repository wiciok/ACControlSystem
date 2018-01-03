using ACCSApi.Model;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Models;
using Newtonsoft.Json.Linq;

namespace ACCSApi.Repositories.Specific
{
    public class ACDeviceRepository : BaseRepository<IACDevice>, IACDeviceRepository
    {
        private static bool isDataGenerated = false;

        public ACDeviceRepository(IDao<IACDevice> deviceDao) : base(deviceDao)
        {
            if (GlobalConfig.GenerateInitialData && !isDataGenerated)
                CreateInitialDataTemp();
        }

        public void CreateInitialDataTemp()
        {
            var turnOff = new ACSetting
            (
                code: new NecCode()
                {
                    Code = "00101000 11000110 00000000 00001000 00001000 01000000 00111111"
                },
                settings: null,
                isTurnOff: true
            );

            var defaultTurnOn = new ACSetting
            (
                code: new NecCode()
                {
                    Code =
                        "00101000 11000110 00000000 00001000 00001000 01111111 10010000 00001100 10001010 10000000 00001100 00000000 00000000 00000000 00000100 01110100"
                },

                settings: new JObject()
                {
                    {"Mode", "Cool"}
                },
                isTurnOff: false
            );


            Add(new ACDevice[]
            {
                new ACDevice()
                {
                    Id = 1,
                    Brand = "Fujitsu",
                    Model = "",
                    ModulationFrequencyInHz = 38,
                    DutyCycle = 0.5,
                    NecCodeSettings = new NecCodeSettings(
                        leadingPulseDuration: 3200,
                        leadingGapDuration: 1600,
                        oneGapDuration: 1200,
                        onePulseDuration: 410,
                        zeroPulseDuration: 410,
                        zeroGapDuration: 410,
                        sendTrailingPulse: true),
                    AvailableSettings = new ACSetting[]{turnOff,defaultTurnOn},
                    DefaultTurnOnSetting = defaultTurnOn,
                    TurnOffSetting = turnOff
                }
            });

            isDataGenerated = true;
        }

        //todo: check if interface here is not causing any problems
        public IACDevice CurrentDevice
        {
            get => this.Get(GlobalConfig.CurrentAcDeviceId);
            set => GlobalConfig.CurrentAcDeviceId = value.Id;
        }
    }
}
