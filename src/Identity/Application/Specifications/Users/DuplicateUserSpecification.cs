using System.Linq.Expressions;
using Common.Application.Specifications;
using Identity.Application.Types.Entities.Users;

namespace Identity.Application.Specifications.Users;

public class DuplicateUserSpecification : Specification<User>
{
    private readonly string _email;

    public DuplicateUserSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email.ToLower() == _email.ToLower();
    }
}