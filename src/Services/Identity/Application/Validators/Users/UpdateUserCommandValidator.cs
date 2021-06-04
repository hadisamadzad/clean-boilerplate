using FluentValidation;
using Identity.Application.Commands.Users;
using Identity.Application.Constants;
using Identity.Domain;

namespace Identity.Application.Validators.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            // User id
            RuleFor(x => x.UserId)
                .Must(x => 0 < x)
                .WithState(_ => Errors.InvalidInputValidationError);

            // Firstname
            RuleFor(x => x.FirstName)
                .Length(2, Defaults.NameLength)
                .When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithState(_ => Errors.InvalidFirstNameValidationError);

            // Lastname
            RuleFor(x => x.LastName)
                .Length(2, Defaults.NameLength)
                .When(x => !string.IsNullOrEmpty(x.LastName))
                .WithState(_ => Errors.InvalidLastNameValidationError);

            // Email
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithState(_ => Errors.InvalidEmailValidationError);
        }
    }
}
