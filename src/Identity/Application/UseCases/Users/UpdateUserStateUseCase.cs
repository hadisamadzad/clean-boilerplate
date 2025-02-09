using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using Identity.Application.Types.Entities;
using MediatR;

namespace Identity.Application.UseCases.Users;

// Handler
internal class UpdateUserStateHandler(IRepositoryManager unitOfWork) :
    IRequestHandler<UpdateUserStateCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateUserStateCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new UpdateUserStateValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByIdAsync(request.UserId);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        // Update
        user.State = request.State;
        user.UpdatedAt = DateTime.UtcNow;

        _ = await unitOfWork.Users.UpdateAsync(user);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}

// Model
public record UpdateUserStateCommand(
    string AdminUserId,
    string UserId,
    UserState State) : IRequest<OperationResult>;

// Model Validator
public class UpdateUserStateValidator : AbstractValidator<UpdateUserStateCommand>
{
    public UpdateUserStateValidator()
    {
        // User id
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithState(_ => Errors.InvalidId);

        // Email
        RuleFor(x => x.State)
            .IsInEnum()
            .WithState(_ => Errors.InvalidEmail);
    }
}
