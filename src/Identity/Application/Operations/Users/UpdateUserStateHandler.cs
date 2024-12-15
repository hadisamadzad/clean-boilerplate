using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Users;

internal class UpdateUserStateHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateUserStateCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateUserStateCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserStateValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        // Update
        user.State = request.State;
        user.UpdatedAt = DateTime.UtcNow;

        _ = await unitOfWork.Users.UpdateAsync(user);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}
