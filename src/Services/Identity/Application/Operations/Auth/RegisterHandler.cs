using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Operations.Auth;

internal class RegisterHandler(
    IUnitOfWork unitOfWork,
    ITransactionalEmailService transactionalEmailService,
    IOptions<ActivationConfig> activationConfig)
    : IRequestHandler<RegisterCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITransactionalEmailService _transactionalEmailService = transactionalEmailService;
    private readonly ActivationConfig _activationConfig = activationConfig.Value;

    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new RegisterValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
        if (user is not null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.DuplicateUsernameError);

        user = new User
        {
            Email = request.Email.ToLower(),
            PasswordHash = PasswordHelper.Hash(request.Password),
            State = UserState.InActive,
            Role = Role.User,
            SecurityStamp = UserHelper.CreateUserStamp(),
            ConcurrencyStamp = UserHelper.CreateUserStamp(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _unitOfWork.Users.Add(user);

        _ = await _unitOfWork.CommitAsync();

        var expiration = ExpirationTimeHelper
            .GetExpirationTime(_activationConfig.LinkLifetimeInDays);

        var activationToken = ActivationTokenHelper
            .GenerateActivationToken(user.Email, expiration);

        var result = new RegisterResult
        {
            UserId = user.Id,
            Email = user.Email,
            ActivationToken = activationToken,
        };

        var email = user.Email;
        var activationLink = string.Format(_activationConfig.LinkFormat, activationToken);

        var @params = new Dictionary<string, string>
            {
                { "Link", activationLink }
            };

        _ = await _transactionalEmailService.SendEmailByTemplateIdAsync(
            _activationConfig.BrevoTemplateId, [email], @params);

        return new OperationResult(OperationStatus.Completed, value: result);
    }
}