using Identity.Application.Helpers;
using Identity.Application.Types.Entities.Users;
using Identity.Domain;

namespace Identity.Database.Seeding.Seeds;

internal static class UserSeed
{
    public static List<User> All =>
        typeof(UserSeed)
        .GetProperties()
        .Where(x => x.Name != "All")
        .Select(x => x.GetValue(null) as User)
        .ToList();

    public static User Owner { get; } = new User()
    {
        Email = "owner",
        IsEmailConfirmed = false,
        FirstName = "",
        LastName = "",
        State = UserState.Active,
        Role = Role.Owner,
        PasswordHash = PasswordHelper.Hash("owner"),
        ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Constants.SecurityStampLength),
        SecurityStamp = StampGenerator.CreateSecurityStamp(Constants.SecurityStampLength),
        LastPasswordChangeTime = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };
}
