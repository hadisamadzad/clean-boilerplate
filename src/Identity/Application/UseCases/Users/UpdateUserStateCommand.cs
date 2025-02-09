using Common.Application.Infrastructure.Operations;
using Identity.Application.Types.Entities;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record UpdateUserStateCommand(string AdminUserId, string UserId, UserState State) : IRequest<OperationResult>;
