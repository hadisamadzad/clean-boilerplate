using Common.Utilities.OperationResult;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.UseCases.Auth;

// Handler
internal class GetUserProfileHandler(IRepositoryManager unitOfWork) : IRequestHandler<GetUserProfileQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(request.RequestedBy))
            return new OperationResult(OperationStatus.Invalid, Errors.InvalidId);

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.RequestedBy);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable, Errors.InvalidId);

        // Mapping
        var response = user.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, Value: response);
    }
}

// Model
public record GetUserProfileQuery(
    string RequestedBy) : IRequest<OperationResult>;