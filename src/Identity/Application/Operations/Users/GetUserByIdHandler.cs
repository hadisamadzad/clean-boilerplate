using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

public class GetUserByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Get
        var entity = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (entity is null)
            return new OperationResult(OperationStatus.Unprocessable, value: Errors.InvalidId);

        // Mapping
        var model = entity.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, value: model);
    }
}
