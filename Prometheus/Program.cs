using Microsoft.AspNetCore;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Prometheus.Extensions;

namespace Prometheus;

public class Program
{
    public static int Main(string[] args)
    {
        ILogger? log = null;
        
        try
        {
            var host = CreateWebHostBuilder(args)
                       .ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           config.Sources.Clear();
                           config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
                           config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                               optional: true, reloadOnChange: false);
                           config.AddJsonFile("secrets/appsettings.json", optional: true, reloadOnChange: false);
                           config.AddAppSettingsJsonFromEnvVariable("APPSETTINGS");
                           config.AddEnvironmentVariables();
                       }).Build();

            log = host.Services.GetService<ILogger<Program>>();
            log?.LogInformation("Application is starting...");

            host.Run();

            return 0;
        }
        catch (Exception ex)
        {
            log?.LogCritical(ex, "Application terminated unexpectedly");
            if (log == null)
            {
                Console.WriteLine(ex);
            }

            return 1;
        }
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               .UseStartup<StartUp>();
}