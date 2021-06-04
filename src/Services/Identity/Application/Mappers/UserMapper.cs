using System.Collections.Generic;
using System.Linq;
using Identity.Application.Models.Responses.Users;
using Identity.Domain.Users;

namespace Identity.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponse MapToUserResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsEmailConfirmed = user.IsEmailConfirmed,
                State = user.State,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Fullname = user.FullName,
                NotificationCount = 0,
                IsLockedOut = user.IsLockedOut,
                LastPasswordChangeDate = user.LastPasswordChangeTime,

                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,

                Roles = user.Roles?.Select(x => x.Role).MapToRoleResponses().ToList(),
            };
        }

        public static IEnumerable<UserResponse> MapToUserResponses(this IEnumerable<User> users)
        {
            foreach (var user in users)
                yield return user.MapToUserResponse();
        }
    }
}