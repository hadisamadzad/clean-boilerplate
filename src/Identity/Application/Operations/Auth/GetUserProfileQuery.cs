using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record GetUserProfileQuery(int RequestedBy) : IRequest<OperationResult>;