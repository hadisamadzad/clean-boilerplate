using Common.Helpers;
using Common.Utilities.OperationResult;
using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.UseCases.Auth;

// Handler
internal class CheckUsernameHandler(IRepositoryManager unitOfWork) : IRequestHandler<CheckUsernameQuery, OperationResult>
{
    public async Task<OperationResult> Handle(CheckUsernameQuery request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CheckUsernameValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByEmailAsync(request.Email);

        var isAvailable = user is null;

        return new OperationResult(OperationStatus.Completed, Value: isAvailable);
    }
}

// Model
public record CheckUsernameQuery(string Email) : IRequest<OperationResult>;

// Model Validator
public class CheckUsernameValidator : AbstractValidator<CheckUsernameQuery>
{
    public CheckUsernameValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);
    }
}