using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Auth;

public record CheckUsernameQuery(string Email) : IRequest<OperationResult>;