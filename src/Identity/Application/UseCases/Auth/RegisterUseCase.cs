using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Common.Helpers;
using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Specifications.Auth;
using Identity.Application.Types.Entities;
using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Auth;
using MediatR;

namespace Identity.Application.UseCases.Auth;

// Handler
internal class RegisterHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new RegisterValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Check initial ownership
        // NOTE Registration is supposed to be done only once and for the first user. So if
        // there is any existing user, it means there is nothing to do with registration.
        var isAlreadyOwned = await unitOfWork.Users.AnyUsersAsync();
        if (isAlreadyOwned)
            return new OperationResult(OperationStatus.Unprocessable, Errors.OwnershipAlreadyDone);

        var user = new UserEntity
        {
            Id = UidHelper.GenerateNewId("user"),
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

// Model
public record RegisterCommand(string Email, string Password) : IRequest<OperationResult>;

// Model Validator
public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);

        RuleFor(x => x.Password)
            .Must(x => new AcceptablePasswordStrengthSpecification().IsSatisfiedBy(x))
            .WithState(_ => Errors.WeakPassword);
    }
}