using Common.Application.Configs;
using Identity.Infrastructure.Database;
using MongoDB.Driver;

namespace Identity.Core.ServiceRegistrations;

public static class MongoDBRegistration
{
    public static IServiceCollection AddConfiguredMongoDB(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection(MongoDBConfig.Key).Get<MongoDBConfig>();

        services.AddSingleton<IMongoDatabase>(MongoDBContext.Connect(config));

        return services;
    }
}