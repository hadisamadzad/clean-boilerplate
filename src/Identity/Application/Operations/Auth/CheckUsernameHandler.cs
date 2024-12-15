using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class CheckUsernameHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckUsernameQuery, OperationResult>
{
    public async Task<OperationResult> Handle(CheckUsernameQuery request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CheckUsernameValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByEmailAsync(request.Email);

        var isAvailable = user is null;

        return new OperationResult(OperationStatus.Completed, value: isAvailable);
    }
}