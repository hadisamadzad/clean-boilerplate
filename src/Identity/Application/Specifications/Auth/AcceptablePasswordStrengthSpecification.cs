using System.Linq.Expressions;
using Common.Application.Specifications;
using Identity.Application.Helpers;

namespace Identity.Application.Specifications.Auth;

public class AcceptablePasswordStrengthSpecification : Specification<string>
{
    public AcceptablePasswordStrengthSpecification()
    {
    }

    public override Expression<Func<string, bool>> ToExpression()
    {
        return _ => PasswordHelper.CheckStrength(_) >= PasswordScore.Medium;
    }
}