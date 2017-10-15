using System.Collections.Generic;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Repositories.Generic;
using ACCSApi.Repositories.Interfaces;
using ACCSApi.Repositories.Models.Settings;

namespace ACCSApi.Repositories.Specific
{
    public class RaspberryPiDeviceRepository : BaseRepository<IRaspberryPiDevice>, IRaspberryPiDeviceRepository
    {
        public RaspberryPiDeviceRepository(IDao<IRaspberryPiDevice> dao) : base(dao)
        {
            CreateInitialDataTemp(); // todo: finally remove this, it should only be persisted in file
        }

        public void CreateInitialDataTemp()
        {
            Add(new RaspberryPiDevice[]
            {
                new RaspberryPiDevice()
                {
                    Id=1,
                    Name="Rasbperry Pi Model 3B",
                    ValidBoardAndBroadcomPins=new Dictionary<uint, uint>()
                    {
                        { 7, 4 },
                        {11,17 },
                        {12,18 },
                        {13,27 },
                        {15,22 },
                        {16,23 },
                        {18,24 },
                        {22,25 },
                        {29, 5 },
                        {31, 6 },
                        {32,12 },
                        {33,13 },
                        {35,19 },
                        {36,16 },
                        {37,26 },
                        {38,20 },
                        {40,21 }
                    },
                    BoardInPin=16,
                    BoardOutPin=18
                }
            });
        }

        public IRaspberryPiDevice CurrentDevice
        {
            get => this.Get(GlobalSettings.currentRaspberryPiDeviceId);
            set => GlobalSettings.currentRaspberryPiDeviceId = value.Id;
        }
    }
}
