using Common.Application.Configs;
using Identity.Infrastructure.Database;

namespace Identity.Core.Bootstrap;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddConfiguredMongoDB(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection(MongoDBConfig.Key).Get<MongoDBConfig>();

        services.AddSingleton(MongoDBContext.Connect(config));

        return services;
    }
}