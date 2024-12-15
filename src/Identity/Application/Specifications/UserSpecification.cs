using System.Linq.Expressions;
using Identity.Application.Types.Entities;

namespace Identity.Application.Specifications;

public static class UserSpecification
{
    public static Expression<Func<UserEntity, bool>> DuplicateUser(string email) =>
        user => user.Email.ToLower() == email.ToLower();
}