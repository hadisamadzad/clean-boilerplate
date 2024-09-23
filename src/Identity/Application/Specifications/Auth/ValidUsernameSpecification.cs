using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Common.Application.Specifications;

namespace Identity.Application.Specifications.Auth;

public class ValidUsernameSpecification : Specification<string>
{
    public ValidUsernameSpecification()
    {
    }

    public override Expression<Func<string, bool>> ToExpression()
    {
        return _ => Regex.Match(_,
            "^(?=.{6,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").Success;
    }
}