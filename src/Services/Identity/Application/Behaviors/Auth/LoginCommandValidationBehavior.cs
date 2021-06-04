using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Models.Commands.Auth;
using Identity.Application.Validators.Auth;
using MediatR;

namespace Identity.Application.Behaviors.Auth
{
    public class LoginCommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<LoginCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(LoginCommand request,
            CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult> next)
        {
            // Validation
            var validation = new LoginCommandValidator().Validate(request);
            if (!validation.IsValid)
                return new OperationResult(OperationResultStatus.InvalidRequest, validation.Errors[0].CustomState);

            return await next();
        }
    }
}