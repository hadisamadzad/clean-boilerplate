namespace Common.Application.Configs;

public class RedisCacheConfig
{
    public const string Key = "RedisCache";

    public string SingleNode { get; set; }
    public string[] ClusterNodes { get; set; }
    public bool ClusterEnabled { get; set; }

    public string[] Connections => ClusterEnabled ? ClusterNodes : new string[] { SingleNode };
}