using ACCSApi.Services.GlobalConfig;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ACCSApi.Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            var configPersister = new GlobalConfigPersister();
            //configPersister.GenerateConfigFile();
            configPersister.LoadGlobalConfigFromFile();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
