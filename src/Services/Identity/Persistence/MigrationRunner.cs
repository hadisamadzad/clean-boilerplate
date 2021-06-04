using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Persistence
{
    internal static class MigrationRunner
    {
        public static void Run(IServiceProvider serviceProvider)
        {
            using var scoped = serviceProvider.CreateScope();
            var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}