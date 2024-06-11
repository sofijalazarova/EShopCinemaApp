using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

//namespace EShop.Web
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            // Early init of NLog to allow startup and exception logging, before host is built
//            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//            logger.Debug("init main");

//            try
//            {
//                var hostBuilder = Host.CreateDefaultBuilder(args);

//                // NLog: Setup NLog for Dependency injection
//                hostBuilder.ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                    webBuilder.ConfigureLogging(logging =>
//                    {
//                        logging.ClearProviders();
//                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
//                    });
//                    webBuilder.UseNLog(); // <- NLog Integration
//                });

//                var host = hostBuilder.Build();
//                host.Run();
//            }
//            catch (Exception exception)
//            {
//                // NLog: catch setup errors
//                logger.Error(exception, "Stopped program because of exception");
//                throw;
//            }
//            finally
//            {
//                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
//                NLog.LogManager.Shutdown();
//            }
//        }
//    }
//}


namespace EShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
