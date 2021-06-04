using System;
using System.Collections.Generic;
using System.Linq;
using Identity.Application.Helpers;
using Identity.Domain;
using Identity.Domain.Roles;
using Identity.Domain.Users;
using Identity.Persistence.Seeding.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Persistence.Seeding
{
    public static class Seeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scoped = serviceProvider.CreateScope();
            var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();

            // Users
            var userSeeds = UserSeed.All;
            var userSeedUsernames = userSeeds.ConvertAll(x => x.Username.ToLower());

            var toBeUpdatedUsers = context.Users
                .Include(x => x.Roles)
                .Where(x => userSeedUsernames.Contains(x.Username.ToLower()))
                .ToList();
            var toBeAddedUsers = userSeeds
                .Where(x => !toBeUpdatedUsers.ConvertAll(y => y.Username).Contains(x.Username));

            foreach (var item in toBeUpdatedUsers)
            {
                var seed = userSeeds.Single(x => x.Username.ToLower() == item.Username.ToLower());

                item.IsEmailConfirmed = seed.IsEmailConfirmed;
                item.FirstName = seed.FirstName;
                item.LastName = seed.LastName;
                item.State = seed.State;
                item.PasswordHash = new PasswordHasher().Hash("owner");
                item.ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength);
                item.SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength);
                item.Roles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = Role.Owner,
                        CreatedAt = DateTime.UtcNow
                    }
                };
                item.UpdatedAt = DateTime.UtcNow;
            }

            foreach (var item in toBeAddedUsers)
            {
                context.Users.Add(item);
            }

            context.SaveChanges();
        }
    }
}