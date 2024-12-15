using Identity.Application.Types.Entities;

namespace Identity.Api.Models.Users;

public record UpdateUserStateRequest(UserState State);