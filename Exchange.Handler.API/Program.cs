using Exchange.Handler.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Handler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");

                IWebHost host = CreateWebHostBuilder(args).Build();

                PrepareDatabase(host, logger);

                logger.Debug("run host");
                host.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:4000")
                .ConfigureLogging((hostingContext, logging) =>
                {
                    if (hostingContext.HostingEnvironment.IsProduction())
                        logging.ClearProviders();

                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddNLog();
                });

        public static void PrepareDatabase(IWebHost host, NLog.ILogger logger)
        {
            logger.Info($"Preparing Credicove database");

            using (IServiceScope serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var appContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        DataSeeder.Seed(appContext);
                    }
                    catch (Exception)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
        }
    }
}
