using Autofac;

namespace ACCSApi.Repositories.Models
{
    public static class GlobalConfig
    {
        public static bool GenerateInitialData = false;
        
        //DAOs:
        public static string PathToKeepFilesWithData = @"./";

        //repositories:

        public static int CurrentRaspberryPiDeviceId = 1;
        public static int CurrentAcDeviceId = 0;

        //Utils:
        public static TokenExpirationType TokenExpirationType = TokenExpirationType.ByTime;
        public static int TokenExpirationTimeInSeconds = 600;

        //Other, shouldnt be serializable
        public static IContainer Container;
    }
}
