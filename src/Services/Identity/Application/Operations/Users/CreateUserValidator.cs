using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.Operations.Users;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        // Email
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmailError);

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithState(_ => Errors.InvalidPasswordValidationError);

        // First name
        RuleFor(x => x.FirstName)
            .MaximumLength(Domain.Constants.Char80Length)
            .WithState(_ => Errors.InvalidFirstNameError);

        // Last name
        RuleFor(x => x.LastName)
            .MaximumLength(Domain.Constants.Char80Length)
            .WithState(_ => Errors.InvalidLastNameError);

    }
}
