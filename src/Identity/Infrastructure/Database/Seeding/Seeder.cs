using Identity.Application.Helpers;
using Identity.Database;
using Identity.Database.Seeding.Seeds;
using Identity.Domain;

namespace Identity.Infrastructure.Database.Seeding;

public static class Seeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scoped = serviceProvider.CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();

        // Users
        var userSeeds = UserSeed.All;
        var userSeedEmails = userSeeds.ConvertAll(x => x.Email.ToLower());

        var toBeUpdatedUsers = context.Users
            .Where(x => userSeedEmails.Contains(x.Email.ToLower()))
            .ToList();
        var toBeAddedUsers = userSeeds
            .Where(x => !toBeUpdatedUsers.ConvertAll(y => y.Email).Contains(x.Email));

        foreach (var item in toBeUpdatedUsers)
        {
            var seed = userSeeds.Single(x => x.Email.ToLower() == item.Email.ToLower());

            item.IsEmailConfirmed = seed.IsEmailConfirmed;
            item.FirstName = seed.FirstName;
            item.LastName = seed.LastName;
            item.State = seed.State;
            item.Role = seed.Role;
            item.PasswordHash = PasswordHelper.Hash("owner");
            item.ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Constants.SecurityStampLength);
            item.SecurityStamp = StampGenerator.CreateSecurityStamp(Constants.SecurityStampLength);
            item.UpdatedAt = DateTime.UtcNow;
        }

        context.Users.AddRange(toBeAddedUsers);

        context.SaveChanges();
    }
}
