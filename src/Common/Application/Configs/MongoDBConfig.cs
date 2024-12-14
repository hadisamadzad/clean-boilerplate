namespace Common.Application.Configs;

public record MongoDBConfig(string ConnectionString, string DatabaseName)
{
    public const string Key = "MongoDB";
}