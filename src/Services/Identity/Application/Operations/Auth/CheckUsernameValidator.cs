using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.Operations.Auth;

public class CheckUsernameValidator : AbstractValidator<CheckUsernameQuery>
{
    public CheckUsernameValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmailError);
    }
}