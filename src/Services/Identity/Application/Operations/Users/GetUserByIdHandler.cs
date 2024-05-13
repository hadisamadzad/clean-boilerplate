using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Get
        var entity = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (entity == null)
            return new OperationResult(OperationStatus.Unprocessable, value: UserErrors.UserNotFoundError);

        // Mapping
        var model = entity.MapToUserModel();

        return new OperationResult(OperationStatus.Completed, value: model);
    }
}
