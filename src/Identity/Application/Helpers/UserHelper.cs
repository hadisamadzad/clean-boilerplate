using Identity.Application.Types.Configs;
using Identity.Application.Types.Entities;
using Identity.Application.Types.Entities.Users;

namespace Identity.Application.Helpers;

public static class UserHelper
{
    public static LockoutConfig LockoutConfig;

    public static string CreateUserStamp() => Guid.NewGuid().ToString("N");

    public static string GetFullName(this UserEntity user) => $"{user.FirstName} {user.LastName}";

    public static void Activate(this UserEntity user)
    {
        user.State = UserState.Active;
    }

    public static bool IsLockedOutOrNotActive(this UserEntity user) =>
        user.State != UserState.Active || user.IsLockedOut();

    public static bool IsLockedOut(this UserEntity user) => user.LockoutEndTime > DateTime.UtcNow;

    public static void TryToLockout(this UserEntity user)
    {
        user.FailedLoginCount++;
        if (user.FailedLoginCount >= LockoutConfig.FailedLoginLimit)
            user.LockoutEndTime = DateTime.UtcNow.Add(LockoutConfig.Duration);
    }

    public static void ResetLockoutHistory(this UserEntity user)
    {
        user.FailedLoginCount = 0;
        user.LockoutEndTime = null;
    }

    public static string CreateJwtAccessToken(this UserEntity user) =>
        JwtHelper.CreateJwtAccessToken(user.Id, user.Email);

    public static string CreateJwtRefreshToken(this UserEntity user) =>
        JwtHelper.CreateJwtRefreshToken(user.Id, user.Email);

    public static bool IsAdmin(this UserEntity user) =>
        user.Role is Role.Admin;
}
