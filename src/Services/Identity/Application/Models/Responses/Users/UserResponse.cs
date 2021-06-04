using System;
using System.Collections.Generic;
using Identity.Application.Models.Responses.Roles;
using Identity.Domain.Users;

namespace Identity.Application.Models.Responses.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public DateTime? LastPasswordChangeDate { get; set; }
        public bool IsLockedOut { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public UserState State { get; set; }
        public long NotificationCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<RoleResponse> Roles { get; set; }
    }
}