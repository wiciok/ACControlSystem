using System;
using System.IO;
using ACCSApi.Services.GlobalConfig;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace ACCSApi.Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath("nlog.config");
            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                logger.Error(e, "Stopped program because of exception");
                throw;
            }

            //var configPersister = new GlobalConfigPersister();
            //configPersister.GenerateConfigFile();
            //configPersister.LoadGlobalConfigFromFile();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:5001")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
    }
}
