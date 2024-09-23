using Identity.Application.Types.Entities.Users;

namespace Identity.Api.Models.Users;

public record UpdateUserStateRequest(UserState State);