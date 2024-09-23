using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserProfileHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        // Validation
        if (request.RequestedBy == 0)
            return new OperationResult(OperationStatus.ValidationFailed,
                Errors.InvalidIdentifierError);

        // Get
        var user = await _unitOfWork.Users.GetUserByIdAsync(request.RequestedBy);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                UserErrors.UserNotFoundError);

        // Mapping
        var response = user.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, value: response);
    }
}
