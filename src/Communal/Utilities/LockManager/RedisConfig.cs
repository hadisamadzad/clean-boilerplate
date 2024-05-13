namespace Communal.Utilities.LockManager;

public class RedisConfig
{
    public string SingleNode { get; set; }
    public string[] ClusterNodes { get; set; }
    public bool ClusterEnabled { get; set; }

    public string[] Connections => ClusterEnabled ? ClusterNodes : [SingleNode];
}