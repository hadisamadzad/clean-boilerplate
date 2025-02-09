using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Specifications;
using Identity.Application.Types.Entities;
using Identity.Application.Types.Entities.Users;
using MediatR;

namespace Identity.Application.UseCases.Users;

internal class CreateUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CreateUserValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Check if user is admin
        var requestingUser = await unitOfWork.Users.GetUserByIdAsync(request.AdminUserId);
        if (requestingUser is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        // Check role
        Role[] adminRoles = [Role.Owner, Role.Admin];
        if (!adminRoles.Contains(requestingUser.Role))
            return new OperationResult(OperationStatus.Unauthorized,
                value: Errors.InsufficientAccessLevel);

        // Checking same email
        var isExist = await unitOfWork.Users
            .ExistsAsync(UserSpecification.DuplicateUser(request.Email));
        if (isExist)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.DuplicateUsername);

        // Factory
        var entity = new UserEntity
        {
            PasswordHash = PasswordHelper.Hash(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            IsEmailConfirmed = true,
            State = UserState.Active,
            Role = Role.User,
            SecurityStamp = UserHelper.CreateUserStamp(),
            ConcurrencyStamp = UserHelper.CreateUserStamp(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await unitOfWork.Users.InsertAsync(entity);

        return new OperationResult(OperationStatus.Completed, value: entity);
    }
}
