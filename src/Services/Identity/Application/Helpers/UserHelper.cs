using System;
using Identity.Application.Configurations;
using Identity.Application.Models.Commands.Users;
using Identity.Domain.Users;

namespace Identity.Application.Helpers
{
    public static class UserHelper
    {
        public static LockoutConfig LockoutConfig;

        public static User CreateUser(CreateUserCommand command) => new User
        {
            Username = command.Username,
            PasswordHash = new PasswordHasher().Hash(command.Password),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            State = UserState.Active,
            SecurityStamp = Guid.NewGuid().ToString("N"),
            ConcurrencyStamp = Guid.NewGuid().ToString("N"),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        public static void Activate(this User user)
        {
            user.State = UserState.Active;
        }

        public static bool CanLogin(this User user) =>
            user.State == UserState.Active &&
            user.Roles.Count > 0 &&
            !(user.LockoutEndTime > DateTime.UtcNow);

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
    }
}