using Common.Application.Infrastructure.Operations;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class CheckRegistrationHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CheckRegistrationQuery, OperationResult>
{
    public async Task<OperationResult> Handle(CheckRegistrationQuery request, CancellationToken cancellationToken)
    {
        // Check initial registration
        var isAlreadyInitialised = await unitOfWork.Users.AnyUsersAsync();

        return new OperationResult(OperationStatus.Completed, value: isAlreadyInitialised);
    }
}