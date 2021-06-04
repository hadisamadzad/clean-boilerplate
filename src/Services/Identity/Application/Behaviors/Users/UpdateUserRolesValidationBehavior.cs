using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Commands.Users;
using Identity.Application.Helpers;
using Identity.Application.Validators.Users;
using MediatR;

namespace Identity.Application.Behaviors.Users
{
    public class UpdateUserRolesValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<UpdateUserRolesCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(UpdateUserRolesCommand request,
            CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult> next)
        {
            // Validation
            var validation = new UpdateUserRolesCommandValidator().Validate(request);
            if (!validation.IsValid)
                return new OperationResult(OperationResultStatus.InvalidRequest, validation.GetFirstErrorMessage());

            return await next();
        }
    }
}