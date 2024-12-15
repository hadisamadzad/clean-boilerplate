using Common.Application.Configs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Identity.Infrastructure.Database;

public class MongoDBContext
{
    public static IMongoDatabase Connect(MongoDBConfig configs)
    {
        Configure();

        var client = new MongoClient(configs.ConnectionString);
        return client.GetDatabase(configs.DatabaseName);
        // TODO https://jira.mongodb.org/browse/CSHARP-1728
    }

    private static void Configure()
    {
        // Set up enum to string convertor (applies to all entities)
        ConventionRegistry.Register("EnumStringConvention",
            new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, x => true);
    }
}