using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Operations.Auth;

internal class SendPasswordResetEmailHandler(
    IUnitOfWork unitOfWork,
    ITransactionalEmailService transactionalEmailService,
    IOptions<PasswordResetConfig> passwordResetConfig)
    : IRequestHandler<SendPasswordResetEmailCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITransactionalEmailService _transactionalEmailService = transactionalEmailService;
    private readonly PasswordResetConfig _passwordResetConfig = passwordResetConfig.Value;

    public async Task<OperationResult> Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new SendPasswordResetEmailValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.UserNotFoundError);

        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.LockoutUserLoginError);

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

        _ = await _transactionalEmailService.SendEmailByTemplateIdAsync(
            _passwordResetConfig.BrevoTemplateId, [email], @params);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}