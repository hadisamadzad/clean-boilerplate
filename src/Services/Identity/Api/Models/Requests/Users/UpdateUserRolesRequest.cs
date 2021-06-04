using System.Collections.Generic;
using Identity.Domain.Roles;

namespace Identity.Api.Models.Requests.Users
{
    public class UpdateUserRolesRequest
    {
        public List<Role> Roles { get; set; }
    }
}
