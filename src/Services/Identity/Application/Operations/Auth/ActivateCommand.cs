using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record ActivateCommand : IRequest<OperationResult>
{
    public string ActivationToken { get; set; }
}