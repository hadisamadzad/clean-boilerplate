using Common.Utilities.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.UseCases.Users;

// Handler
public class GetUserByIdHandler(IRepositoryManager unitOfWork) : IRequestHandler<GetUserByIdQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Get
        var entity = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (entity is null)
            return new OperationResult(OperationStatus.Unprocessable, Value: Errors.InvalidId);

        // Mapping
        var model = entity.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, Value: model);
    }
}

// Model
public record GetUserByIdQuery(string UserId) : IRequest<OperationResult>;