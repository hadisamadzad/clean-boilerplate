using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Specifications.Users;
using Identity.Application.Types.Entities.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

internal class CreateUserHandler : IRequestHandler<CreateUserCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Adding user is only available by registration and invitation");

        // Validation
        var validation = new CreateUserValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Checking same email
        var isExist = await _unitOfWork.Users
            .ExistsAsync(new DuplicateUserSpecification(request.Email).ToExpression());
        if (isExist)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.DuplicateUsernameError);

        // Factory
        var entity = new User
        {
            PasswordHash = PasswordHelper.Hash(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            State = UserState.Active,
            Role = Role.User,
            SecurityStamp = UserHelper.CreateUserStamp(),
            ConcurrencyStamp = UserHelper.CreateUserStamp(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _unitOfWork.Users.Add(entity);

        return new OperationResult(OperationStatus.Completed, value: entity);
    }
}
