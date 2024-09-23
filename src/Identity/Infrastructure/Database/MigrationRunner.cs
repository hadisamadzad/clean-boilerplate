using Identity.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Database;

internal static class MigrationRunner
{
    public static void Run(IServiceProvider serviceProvider)
    {
        using var scoped = serviceProvider.CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}