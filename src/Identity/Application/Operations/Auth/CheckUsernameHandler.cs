using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class CheckUsernameHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckUsernameQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OperationResult> Handle(CheckUsernameQuery request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CheckUsernameValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);

        var duplicated = user != null;

        if (duplicated)
            return new OperationResult(OperationStatus.Unprocessable, value: UserErrors.DuplicateUsernameError);

        return new OperationResult(OperationStatus.Completed, value: true);
    }
}