using System.Collections.Generic;
using Identity.Application.Models.Responses.Roles;
using Identity.Domain.Roles;

namespace Identity.Application.Mappers
{
    public static class RoleMapper
    {
        public static RoleResponse MapToRoleResponse(this Role role)
        {
            return new RoleResponse
            {
                Code = (int)role,
                Name = nameof(role)
            };
        }

        public static IEnumerable<RoleResponse> MapToRoleResponses(this IEnumerable<Role> roles)
        {
            foreach (var role in roles)
                yield return role.MapToRoleResponse();
        }
    }
}