using Identity.Api.Extensions.DependencyInjection;
using Identity.Extensions.DependencyInjection;
using Identity.Extensions.Middleware;
using Identity.Persistence;
using Identity.Persistence.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfigurations(Configuration);
            services.AddConfiguredDatabase(Configuration);

            services.AddServices();
            services.AddConfiguredMediatR();

            services.AddConfiguredMassTransit(Configuration);
            services.AddConfiguredHealthChecks();
            services.AddConfiguredSwagger();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseConfiguredExceptionHandler(env);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });

            if (!env.IsProduction())
                app.UseConfiguredSwagger();

            MigrationRunner.Run(app.ApplicationServices);
            Seeder.Seed(app.ApplicationServices);
        }
    }
}