using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Entities.Users;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class ActivateHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<ActivateCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OperationResult> Handle(ActivateCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, email) = ActivationTokenHelper.ReadActivationToken(request.ActivationToken);

        if (!succeeded)
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InvalidActivationTokenError);

        var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
        if (user is null)
            throw new AggregateException($"Unable to read the valid activation token: {request.ActivationToken}");

        if (user.State != UserState.InActive)
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InvalidUserStateForActivationTokenError);

        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InconsistentTokenError);

        user.State = UserState.Active;
        user.IsEmailConfirmed = true;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);

        _ = await _unitOfWork.CommitAsync();

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}
