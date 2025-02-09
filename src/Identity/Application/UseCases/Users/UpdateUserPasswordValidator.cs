using FluentValidation;
using Identity.Application.Constants.Errors;
using Identity.Application.Specifications.Auth;

namespace Identity.Application.UseCases.Users;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithState(_ => Errors.InvalidPassword);

        RuleFor(x => x.NewPassword)
            .Must(x => new AcceptablePasswordStrengthSpecification().IsSatisfiedBy(x))
            .WithState(_ => Errors.WeakPassword);
    }
}
