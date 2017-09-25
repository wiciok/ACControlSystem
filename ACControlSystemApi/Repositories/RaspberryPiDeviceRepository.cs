using ACControlSystemApi.Repositories.Generic;
using ACControlSystemApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories
{
    public interface IRaspberryPiDeviceRepository : IRepository<RaspberryPiDevice>
    {
        RaspberryPiDevice CurrentDevice { get; set; }
    }

    public class RaspberryPiDeviceRepository : BaseRepository<RaspberryPiDevice>, IRaspberryPiDeviceRepository
    {
        private RaspberryPiDevice _currentDevice;

        public RaspberryPiDeviceRepository(IDao<RaspberryPiDevice> dao) : base(dao)
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

        public RaspberryPiDevice CurrentDevice
        {
            get => this.Get(GlobalSettings.currentRaspberryPiDeviceId);
            set => GlobalSettings.currentRaspberryPiDeviceId = value.Id;
        }
    }
}
