using System;
using System.Linq.Expressions;
using BankManagement.Application.Specifications;
using Identity.Domain.Users;

namespace Identity.Application.Specifications.Users
{
    public class DuplicateUserSpecification : Specification<User>
    {
        private readonly string _username;

        public DuplicateUserSpecification(string username)
        {
            _username = username;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Username.ToLower() == _username.ToLower();
        }
    }
}