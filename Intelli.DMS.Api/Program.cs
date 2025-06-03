using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Config;
using NLog.Extensions.Logging;

namespace Intelli.DMS.Api
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            ConfigurationItemFactory.Default.LayoutRenderers
          .RegisterDefinition("epoch", typeof(NLog.LayoutRenderers.EPOCHDateLayoutRenderer));
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>An IHostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddNLog(hostingContext.Configuration.GetSection("Logging"));
                    });
                });
    }
}
