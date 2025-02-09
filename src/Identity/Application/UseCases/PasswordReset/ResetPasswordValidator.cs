using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Specifications.Auth;

namespace Identity.Application.UseCases.PasswordReset;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithState(_ => Errors.InvalidToken);

        RuleFor(x => x.NewPassword)
            .Must(x => new AcceptablePasswordStrengthSpecification().IsSatisfiedBy(x))
            .WithState(_ => Errors.WeakPassword);
    }
}