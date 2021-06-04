using System;
using System.Collections.Generic;
using System.Linq;
using Identity.Application.Helpers;
using Identity.Domain;
using Identity.Domain.Roles;
using Identity.Domain.Users;

namespace Identity.Persistence.Seeding.Seeds
{
    internal static class UserSeed
    {
        public static List<User> All => typeof(UserSeed).GetProperties().Where(x => x.Name != "All")
            .Select(x => x.GetValue(null) as User).ToList();

        public static User Owner { get; } = new User()
        {
            IsEmailConfirmed = false,
            FirstName = "",
            LastName = "",
            State = UserState.Active,
            Username = "owner",
            PasswordHash = new PasswordHasher().Hash("owner"),
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            LastPasswordChangeTime = DateTime.UtcNow,
            Roles = new List<UserRole>
            {
                new UserRole
                {
                    Role = Role.Owner,
                    CreatedAt = DateTime.UtcNow
                }
            },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}