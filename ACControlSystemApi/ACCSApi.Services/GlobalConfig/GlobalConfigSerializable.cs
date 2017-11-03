using ACCSApi.Repositories.Models;

namespace ACCSApi.Services.GlobalConfig
{
    internal class GlobalConfigSerializable
    {
        public bool GenerateInitialData { get; set; }

        //DAOs:
        public string PathToKeepFilesWithData { get; set; }

        //repositories:

        public int CurrentRaspberryPiDeviceId { get; set; }
        public int CurrentAcDeviceId { get; set; }

        //Utils:
        public TokenExpirationType TokenExpirationType { get; set; }

        public int TokenExpirationTimeInSeconds { get; set; }
    }
}
