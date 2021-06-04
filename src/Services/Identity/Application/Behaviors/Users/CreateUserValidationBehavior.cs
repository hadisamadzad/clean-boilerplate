using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Helpers;
using Identity.Application.Models.Commands.Users;
using Identity.Application.Validators.Users;
using MediatR;

namespace Identity.Application.Behaviors.Users
{
    public class CreateUserValidationBehavior<TRequest, TResponse> : IPipelineBehavior<CreateUserCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(CreateUserCommand request,
            CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult> next)
        {
            // Validation
            var validation = new CreateUserCommandValidator().Validate(request);
            if (!validation.IsValid)
                return new OperationResult(OperationResultStatus.InvalidRequest, validation.GetFirstErrorMessage());

            return await next();
        }
    }
}