using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Users;

internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.UserNotFoundError);

        // Update
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        user.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Users.Update(user);

        await _unitOfWork.CommitAsync();

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}