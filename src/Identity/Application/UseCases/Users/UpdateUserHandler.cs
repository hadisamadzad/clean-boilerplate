using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.UseCases.Users;

internal class UpdateUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        // Update
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        user.UpdatedAt = DateTime.UtcNow;
        _ = await unitOfWork.Users.UpdateAsync(user);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}