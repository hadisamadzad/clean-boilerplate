using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Common.Helpers;
using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Specifications;
using Identity.Application.Types.Entities;
using Identity.Application.Types.Entities.Users;
using MediatR;

namespace Identity.Application.UseCases.Users;

// Handler
internal class CreateUserHandler(IRepositoryManager unitOfWork) : IRequestHandler<CreateUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CreateUserValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Check if user is admin
        var requesterUser = await unitOfWork.Users.GetUserByIdAsync(request.AdminUserId);
        if (requesterUser is null)
            return new OperationResult(OperationStatus.Unprocessable, value: Errors.InvalidId);

        // Check role
        if (!requesterUser.HasAdminRole())
            return new OperationResult(OperationStatus.Unauthorized,
                value: Errors.InsufficientAccessLevel);

        // Checking duplicate email
        var isDuplicate = await unitOfWork.Users
            .ExistsAsync(UserSpecification.DuplicateUser(request.Email));
        if (isDuplicate)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.DuplicateUsername);

        // Factory
        var entity = new UserEntity
        {
            Id = UidHelper.GenerateNewId("user"),
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

// Model
public record CreateUserCommand(
    string AdminUserId,
    string Email,
    string Password) : IRequest<OperationResult>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

// Model Validator
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        // Id
        RuleFor(x => x.AdminUserId)
            .NotEmpty()
            .WithState(_ => Errors.InvalidId);

        // Email
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithState(_ => Errors.InvalidPassword);

        // First name
        RuleFor(x => x.FirstName)
            .MaximumLength(80)
            .WithState(_ => Errors.InvalidFirstName);

        // Last name
        RuleFor(x => x.LastName)
            .MaximumLength(80)
            .WithState(_ => Errors.InvalidLastName);

    }
}