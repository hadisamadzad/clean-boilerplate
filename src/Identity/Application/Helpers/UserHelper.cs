using Identity.Application.Types.Configs;
using Identity.Application.Types.Entities.Users;

namespace Identity.Application.Helpers;

public static class UserHelper
{
    public static LockoutConfig LockoutConfig;

    public static string CreateUserStamp() => Guid.NewGuid().ToString("N");

    public static string GetFullName(this User user) => $"{user.FirstName} {user.LastName}";

    public static void Activate(this User user)
    {
        user.State = UserState.Active;
    }

    public static bool IsLockedOutOrNotActive(this User user) =>
        user.State != UserState.Active || user.IsLockedOut();

    public static bool IsLockedOut(this User user) => user.LockoutEndTime > DateTime.UtcNow;

    public static void TryToLockout(this User user)
    {
        user.FailedLoginCount++;
        if (user.FailedLoginCount >= LockoutConfig.FailedLoginLimit)
            user.LockoutEndTime = DateTime.UtcNow.Add(LockoutConfig.Duration);
    }

    public static void ResetLockoutHistory(this User user)
    {
        user.FailedLoginCount = 0;
        user.LockoutEndTime = null;
    }

    public static string CreateJwtAccessToken(this User user) =>
        JwtHelper.CreateJwtAccessToken(user.Id, user.Email);

    public static string CreateJwtRefreshToken(this User user) =>
        JwtHelper.CreateJwtRefreshToken(user.Id, user.Email);

    public static bool IsAdmin(this User user) =>
        user.Role is Role.Admin;
}
