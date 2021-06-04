using FluentValidation;
using Identity.Application.Constants;
using Identity.Application.Models.Commands.Auth;

namespace Identity.Application.Validators.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Email))
                .WithState(_ => Errors.InvalidEmailValidationError);

            RuleFor(x => x.Username)
                .EmailAddress()
                .When(x => string.IsNullOrEmpty(x.Username))
                .WithState(_ => Errors.InvalidUsernameValidationError);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithState(_ => Errors.InvalidPasswordValidationError);
        }
    }
}
