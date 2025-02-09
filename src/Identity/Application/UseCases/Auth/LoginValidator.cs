using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.UseCases.Auth;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithState(_ => Errors.InvalidPassword);
    }
}
