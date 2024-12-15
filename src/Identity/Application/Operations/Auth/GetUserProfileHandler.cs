using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class GetUserProfileHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserProfileQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        // Validation
        if (request.RequestedBy == 0)
            return new OperationResult(OperationStatus.Invalid,
                Errors.InvalidId);

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.RequestedBy);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable, Errors.InvalidId);

        // Mapping
        var response = user.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, value: response);
    }
}
