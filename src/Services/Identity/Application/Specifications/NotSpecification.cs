using System;
using System.Linq;
using System.Linq.Expressions;

namespace Identity.Application.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;

        public NotSpecification(Specification<T> left)
        {
            _left = left;
        }

        public override Expression<Func<T , bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();

            var notExpression = Expression.Not(leftExpression.Body);

            return Expression.Lambda<Func<T , bool>>(notExpression, leftExpression.Parameters.Single());
        }
    }
}