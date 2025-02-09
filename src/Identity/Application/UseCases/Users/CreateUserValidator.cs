using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.UseCases.Users;

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
            .MaximumLength(80)
            .WithState(_ => Errors.InvalidFirstName);

        // Last name
        RuleFor(x => x.LastName)
            .MaximumLength(80)
            .WithState(_ => Errors.InvalidLastName);

    }
}
