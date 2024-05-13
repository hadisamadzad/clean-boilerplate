using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Users;

internal class UpdateUserStateHandler : IRequestHandler<UpdateUserStateCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserStateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpdateUserStateCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserStateValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.UserNotFoundError);

        // Update
        user.State = request.State;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);

        _ = await _unitOfWork.CommitAsync();

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}
