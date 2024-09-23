using Identity.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.App.Registrations;

public static class DatabaseRegistration
{
    public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Data Context
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        return services;
    }
}