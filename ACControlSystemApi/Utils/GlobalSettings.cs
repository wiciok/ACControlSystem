using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Utils
{
    public static class GlobalSettings
    {
        //todo: repo for persisting this class
        
        //DAOs:
        public static readonly string PathToKeepFilesWithData = "./data/";

        //repositories:

        public static int currentRaspberryPiDeviceId = 1;
    }
}
