using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class CheckUsernameHandler : IRequestHandler<CheckUsernameQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckUsernameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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