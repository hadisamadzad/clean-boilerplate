using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Auth;

public record GetUserProfileQuery(int RequestedBy) : IRequest<OperationResult>;