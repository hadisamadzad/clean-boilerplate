using Common.Helpers;
using Common.Utilities.Operations;
using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.UseCases.PasswordReset;

// Handler
internal class SendPasswordResetEmailHandler(
    IRepositoryManager unitOfWork,
    IEmailService transactionalEmailService,
    IOptions<PasswordResetConfig> passwordResetConfig)
    : IRequestHandler<SendPasswordResetEmailCommand, OperationResult>
{
    private readonly PasswordResetConfig _passwordResetConfig = passwordResetConfig.Value;

    public async Task<OperationResult> Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new SendPasswordResetEmailValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                Value: Errors.InvalidId);

        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable,
                Value: Errors.LockedUser);

        var expirationTime = ExpirationTimeHelper
            .GetExpirationTime(_passwordResetConfig.LinkLifetimeInDays);

        var token = PasswordResetTokenHelper
            .GeneratePasswordResetToken(user.Email, expirationTime);

        var email = user.Email;
        var passwordResetLink = string.Format(_passwordResetConfig.LinkFormat, token);

        var @params = new Dictionary<string, string>
            {
                { "Link", passwordResetLink }
            };

        _ = await transactionalEmailService.SendEmailByTemplateIdAsync(
            _passwordResetConfig.BrevoTemplateId, [email], @params);

        return new OperationResult(OperationStatus.Completed, Value: user.Id);
    }
}

// Model
public record SendPasswordResetEmailCommand(string Email) : IRequest<OperationResult>;

// Model Validator
public class SendPasswordResetEmailValidator : AbstractValidator<SendPasswordResetEmailCommand>
{
    public SendPasswordResetEmailValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);
    }
}