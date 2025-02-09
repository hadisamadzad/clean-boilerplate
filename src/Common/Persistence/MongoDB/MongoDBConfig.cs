namespace Common.Persistence.MongoDB;

public record MongoDBConfig(string ConnectionString, string DatabaseName)
{
    public const string Key = "MongoDB";
}