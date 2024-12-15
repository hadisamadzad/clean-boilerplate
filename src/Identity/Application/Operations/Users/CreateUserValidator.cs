using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.Operations.Users;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        // Id
        RuleFor(x => x.AdminUserId)
            .GreaterThan(0)
            .WithState(_ => Errors.InvalidId);

        // Email
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithState(_ => Errors.InvalidEmail);

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithState(_ => Errors.InvalidPassword);

        // First name
        RuleFor(x => x.FirstName)
            .MaximumLength(Types.Entities.Constants.Char80Length)
            .WithState(_ => Errors.InvalidFirstName);

        // Last name
        RuleFor(x => x.LastName)
            .MaximumLength(Types.Entities.Constants.Char80Length)
            .WithState(_ => Errors.InvalidLastName);

    }
}
