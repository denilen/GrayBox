using System.Text;

namespace Prometheus.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppSettingsJsonFromEnvVariable(this IConfigurationBuilder config,
                                                                              string variableName)
        {
            var appSettings = Environment.GetEnvironmentVariable(variableName);

            if (appSettings == null) return config;

            Directory.CreateDirectory("inmemory");
            File.WriteAllText("inmemory/appsettings.json", appSettings, Encoding.UTF8);
            config.AddJsonFile("inmemory/appsettings.json", optional: false, reloadOnChange: false);

            return config;
        }
    }
}