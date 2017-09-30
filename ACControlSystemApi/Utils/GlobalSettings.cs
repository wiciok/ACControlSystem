﻿using ACControlSystemApi.Utils.Tokens.Enums;
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
        public static int currentACDeviceId = 1;

        //Utils:
        //todo: configure DI to use this!
        public static TokenExpirationType TokenExpirationType = TokenExpirationType.ByTime;
        public static int TokenExpirationTimeInSeconds = 10;
    }
}
