using System.IO;
using ACCSApi.Services.Models;
using Newtonsoft.Json;

namespace ACCSApi.Services.GlobalConfig
{
    public class GlobalConfigPersister
    {
        public const string ConfigFilePath = @"./GlobalConfig.json";

        public void LoadGlobalConfigFromFile()
        {
            try
            {
                GlobalConfigSerializable conf;
                using (JsonReader file = new JsonTextReader(File.OpenText(ConfigFilePath)))
                {
                    var serializer = JsonSerializer.CreateDefault();
                    conf = serializer.Deserialize<GlobalConfigSerializable>(file);
                }

                Repositories.Models.Settings.GlobalConfig.TokenExpirationType = conf.TokenExpirationType;
                Repositories.Models.Settings.GlobalConfig.PathToKeepFilesWithData = conf.PathToKeepFilesWithData;
                Repositories.Models.Settings.GlobalConfig.GenerateInitialData = conf.GenerateInitialData;
                Repositories.Models.Settings.GlobalConfig.TokenExpirationTimeInSeconds = conf.TokenExpirationTimeInSeconds;
                Repositories.Models.Settings.GlobalConfig.currentACDeviceId = conf.CurrentAcDeviceId;
                Repositories.Models.Settings.GlobalConfig.currentRaspberryPiDeviceId = conf.CurrentRaspberryPiDeviceId;
            }
            catch (FileNotFoundException e)
            {
                //todo: logging
            }
        }

        public void GenerateConfigFile()
        {
            var conf = new GlobalConfigSerializable
            {
                TokenExpirationType = Repositories.Models.Settings.GlobalConfig.TokenExpirationType,
                PathToKeepFilesWithData = Repositories.Models.Settings.GlobalConfig.PathToKeepFilesWithData,
                GenerateInitialData = Repositories.Models.Settings.GlobalConfig.GenerateInitialData,
                TokenExpirationTimeInSeconds = Repositories.Models.Settings.GlobalConfig.TokenExpirationTimeInSeconds,
                CurrentRaspberryPiDeviceId = Repositories.Models.Settings.GlobalConfig.currentRaspberryPiDeviceId,
                CurrentAcDeviceId = Repositories.Models.Settings.GlobalConfig.currentACDeviceId
            };

            using (StreamWriter file = File.CreateText(ConfigFilePath))
            {
                var serializer = JsonSerializer.CreateDefault();
                serializer.Serialize(file, conf);
            }
        }
    }
}
