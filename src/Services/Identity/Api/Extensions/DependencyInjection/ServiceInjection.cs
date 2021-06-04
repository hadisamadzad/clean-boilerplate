using Identity.Application.Interfaces;
using Identity.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Api.Extensions.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}