using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.UseCases.Users;

public class UpdateUserPasswordHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateUserPasswordCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserPasswordValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        if (PasswordHelper.CheckPasswordHash(user.PasswordHash, request.CurrentPassword))
            user.PasswordHash = PasswordHelper.Hash(request.NewPassword);
        else
            return new OperationResult(OperationStatus.Unprocessable, value: Errors.InvalidCredentials);

        user.UpdatedAt = DateTime.UtcNow;
        _ = await unitOfWork.Users.UpdateAsync(user);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}
