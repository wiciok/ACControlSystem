using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace ACCSApi.Repositories.Models.Settings
{
    public static class GlobalConfig
    {
        //todo: repo for persisting this class

        public static bool GenerateInitialData = false;
        
        //DAOs:
        public static string PathToKeepFilesWithData = @"./";

        //repositories:

        public static int currentRaspberryPiDeviceId = 1;
        public static int currentACDeviceId = 1;

        //Utils:
        //todo: configure DI to use this!
        public static TokenExpirationType TokenExpirationType = TokenExpirationType.ByTime;
        public static int TokenExpirationTimeInSeconds = 10;

        //Other, shouldnt be serializable
        public static IContainer container;
    }
}
