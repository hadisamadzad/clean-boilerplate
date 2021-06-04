using System;
using System.Linq.Expressions;

namespace Identity.Application.Infrastructure.PropertyMetadata
{
    public static class PropertyHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression)?.Member.Name;
        }

        public static Type GetPropertyType<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression)?.Type;
        }

        public static string GetPropertyTypeName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression)?.Type.Name;
        }

        public static Type GetPropertyClassName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression)?.Member.DeclaringType;
        }

        public static object GetPropertyValue<T>(Expression<Func<T>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();

            return getter();
        }
    }
}
