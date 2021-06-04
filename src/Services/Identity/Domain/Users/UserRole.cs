using System;
using Identity.Domain.Roles;

namespace Identity.Domain.Users
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}