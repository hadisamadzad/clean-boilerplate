using Communal.Application.Infrastructure.Operations;
using Communal.Application.Helpers;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Users;

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserPasswordHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserPasswordValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.UserNotFoundError);

        if (PasswordHelper.CheckPasswordHash(user.PasswordHash, request.CurrentPassword))
            user.PasswordHash = PasswordHelper.Hash(request.NewPassword);
        else
            return new OperationResult(OperationStatus.Unprocessable, value: AuthErrors.InvalidCredentialsError);

        user.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Users.Update(user);

        _ = await _unitOfWork.CommitAsync();

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}
