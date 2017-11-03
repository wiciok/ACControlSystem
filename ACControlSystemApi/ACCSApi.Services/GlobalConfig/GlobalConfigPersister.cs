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

                Repositories.Models.GlobalConfig.TokenExpirationType = conf.TokenExpirationType;
                Repositories.Models.GlobalConfig.PathToKeepFilesWithData = conf.PathToKeepFilesWithData;
                Repositories.Models.GlobalConfig.GenerateInitialData = conf.GenerateInitialData;
                Repositories.Models.GlobalConfig.TokenExpirationTimeInSeconds = conf.TokenExpirationTimeInSeconds;
                Repositories.Models.GlobalConfig.CurrentAcDeviceId = conf.CurrentAcDeviceId;
                Repositories.Models.GlobalConfig.CurrentRaspberryPiDeviceId = conf.CurrentRaspberryPiDeviceId;
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
                TokenExpirationType = Repositories.Models.GlobalConfig.TokenExpirationType,
                PathToKeepFilesWithData = Repositories.Models.GlobalConfig.PathToKeepFilesWithData,
                GenerateInitialData = Repositories.Models.GlobalConfig.GenerateInitialData,
                TokenExpirationTimeInSeconds = Repositories.Models.GlobalConfig.TokenExpirationTimeInSeconds,
                CurrentRaspberryPiDeviceId = Repositories.Models.GlobalConfig.CurrentRaspberryPiDeviceId,
                CurrentAcDeviceId = Repositories.Models.GlobalConfig.CurrentAcDeviceId
            };

            using (StreamWriter file = File.CreateText(ConfigFilePath))
            {
                var serializer = JsonSerializer.CreateDefault();
                serializer.Serialize(file, conf);
            }
        }
    }
}
