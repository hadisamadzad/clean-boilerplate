using FluentValidation;
using Identity.Application.Commands.Users;
using Identity.Application.Constants;

namespace Identity.Application.Validators.Users
{
    public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesCommandValidator()
        {
            // User id
            RuleFor(x => x.UserId)
                .Must(x => 0 < x)
                .WithState(_ => Errors.InvalidInputValidationError);
        }
    }
}
