namespace AddMetaDataMongo.Configuration;

public class MongoConfig
{
    public string Name { get; init; } = string.Empty;
    public string Hosts { get; init; } = string.Empty;
    public int Port { get; init; } = -1;
    public int MaxConnectionPoolSize { get; init; } = 1000;
    public int ConnectTimeout { get; init; } = 30;
    public int ReadPreferenceMode { get; init; } = 3;
    public string? ReplicaSet { get; init; }
    public bool IsSSL { get; init; }
    public bool VerifySslCertificate { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string AdminDatabase { get; init; } = "admin";
    public bool UseTags { get; init; }
    public string? TagsEnviromentName { get; init; }
    public string? Tags { get; init; }
    public bool UseVault { get; init; }
    public int MinConnectionPoolSize { get; init; }
    public int? WaitQueueSize { get; init; }
    public int? MaxConnecting { get; init; }
}
