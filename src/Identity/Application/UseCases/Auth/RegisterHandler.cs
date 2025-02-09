using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Entities;
using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;

namespace Identity.Application.UseCases.Auth;

public record RegisterCommand(string Email, string Password) : IRequest<OperationResult>;

internal class RegisterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new RegisterValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Check initial registration
        // NOTE Registration is supposed to be done only once and for the first user. So if
        // there is any existing user, it means there is nothing to do with registration.
        var isAlreadyInitialized = await unitOfWork.Users.AnyUsersAsync();
        if (isAlreadyInitialized)
            return new OperationResult(OperationStatus.Unprocessable, Errors.RegistrationAlreadyDone);

        var user = new UserEntity
        {
            Email = request.Email.ToLower(),
            PasswordHash = PasswordHelper.Hash(request.Password),
            State = UserState.Active,
            Role = Role.Owner, // The first user will be the owner
            SecurityStamp = UserHelper.CreateUserStamp(),
            ConcurrencyStamp = UserHelper.CreateUserStamp(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await unitOfWork.Users.InsertAsync(user);

        var result = new RegisterResult
        {
            UserId = user.Id,
            Email = user.Email
        };

        return new OperationResult(OperationStatus.Completed, value: result);
    }
}