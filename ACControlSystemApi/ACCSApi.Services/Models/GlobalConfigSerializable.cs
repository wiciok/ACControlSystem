using ACCSApi.Repositories.Models.Settings;

namespace ACCSApi.Services.Models
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
