using FluentValidation;
using Identity.Application.Constants;
using Identity.Application.Models.Commands.Users;
using Identity.Domain;

namespace Identity.Application.Validators.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // Username
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithState(_ => Errors.InvalidUsernameValidationError);
            RuleFor(x => x.Username)
                .Length(2, Defaults.UsernameLength)
                .WithState(_ => Errors.InvalidUsernameValidationError);

            // Password
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithState(_ => Errors.InvalidPasswordValidationError);

            // Firstname
            RuleFor(x => x.FirstName)
                .MaximumLength(Defaults.NameLength)
                .WithState(_ => Errors.InvalidFirstNameValidationError);

            // Lastname
            RuleFor(x => x.LastName)
                .MaximumLength(Defaults.NameLength)
                .WithState(_ => Errors.InvalidLastNameValidationError);

            // Email
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithState(_ => Errors.InvalidEmailValidationError);
        }
    }
}
