using System;
using System.Collections.Generic;
using Identity.Application.Infrastructure.PartialUpdates;

namespace Identity.Domain.Users
{
    public partial class User : IEntity, IPartialUpdate
    {
        #region Identity

        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        #endregion

        #region Login

        public string PasswordHash { get; set; }
        public DateTime? LastPasswordChangeTime { get; set; }

        public int FailedLoginCount { get; set; }
        public DateTime? LockoutEndTime { get; set; }

        public DateTime? LastLoginDate { get; set; }

        #endregion

        #region Profile

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserState State { get; set; }

        #endregion

        #region Management

        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        #endregion

        public ICollection<UserRole> Roles { get; set; }
    }
}