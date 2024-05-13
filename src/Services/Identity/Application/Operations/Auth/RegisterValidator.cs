using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Specifications.Auth;

namespace Identity.Application.Operations.Auth;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmailError);

        RuleFor(x => x.Password)
            .Must(x => new AcceptablePasswordStrengthSpecification().IsSatisfiedBy(x))
            .WithState(_ => Errors.NotSecurePasswordValidationError);
    }
}
