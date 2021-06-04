using Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions.DependencyInjection
{
    public static class DatabaseInjection
    {
        public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Data Context
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

            return services;
        }
    }
}