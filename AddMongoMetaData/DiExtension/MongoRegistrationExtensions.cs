using AddMetaDataMongo.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AddMetaDataMongo.DiExtension;

internal static class MongoRegistrationExtensions
{
    internal static IServiceCollection RegisterMongo(this IServiceCollection services,
                                                     IConfiguration configuration)
    {
        var mongoConfig = configuration.GetConfig<MongoConfig>();

        if (mongoConfig is null)
            throw new InvalidOperationException("Mongo configuration is not filled");

        var mongoClientSettings = new MongoClientSettings
        {
            ApplicationName = mongoConfig.Name,
            Servers = GetServers(mongoConfig.Hosts, mongoConfig.Port),
            MaxConnectionPoolSize = mongoConfig.MaxConnectionPoolSize,
            MinConnectionPoolSize = mongoConfig.MinConnectionPoolSize,
            ConnectTimeout = TimeSpan.FromSeconds(mongoConfig.ConnectTimeout),
            UseTls = mongoConfig.IsSSL,
            AllowInsecureTls = mongoConfig.VerifySslCertificate,
            Credential = !string.IsNullOrEmpty(mongoConfig.UserName) && !string.IsNullOrEmpty(mongoConfig.Password)
                ? MongoCredential.CreateCredential(mongoConfig.AdminDatabase, mongoConfig.UserName, mongoConfig.Password)
                : null,
            ReadPreference = ReadPreference.PrimaryPreferred,
            ReplicaSetName = mongoConfig.ReplicaSet,
            WaitQueueSize = mongoConfig.WaitQueueSize ?? 500,
            MaxConnecting = mongoConfig.MaxConnecting ?? 2
        };

        var mongoClient = new MongoClient(mongoClientSettings);

        return services
            .AddSingleton<IMongoClient>(mongoClient);

    }

    private static T? GetConfig<T>(this IConfiguration configuration,
                                    string? mainSectionName = default,
                                    string? sectionName = default)
    {
        sectionName ??= typeof(T).Name;

        var section = string.IsNullOrWhiteSpace(mainSectionName) ? configuration.GetRequiredSection(sectionName)
            : configuration
                .GetRequiredSection(mainSectionName)
                .GetRequiredSection(sectionName);

        return section.GetConfig<T>();// .Get<T>();
    }

    private static List<MongoServerAddress> GetServers(string servers, int port)
    {
        var serversAddress = servers.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return serversAddress
                .Select(s => port < 0 ? new MongoServerAddress(s) : new MongoServerAddress(s, port))
                .ToList()
            ;
    }
}
