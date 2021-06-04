using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Identity.Application.Infrastructure.PartialUpdates;
using Identity.Application.Infrastructure.PropertyMetadata;

namespace Identity.Application.Infrastructure.PropertyMetadata
{
    public static class ReflectionHelper
    {
        public static UpdateType GetUpdateType(this PropertyInfo property)
        {
            var attribute = property
                .GetCustomAttributes(true)
                .SingleOrDefault(x => x.GetType() == typeof(UpdateAttribute)) as UpdateAttribute;
            return attribute?.GetUpdateType() ?? UpdateType.NormalUpdate;
        }

        // Gets type of update restriction which is applied to a property
        public static UpdateType GetUpdateType<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertyHelper.GetPropertyName(propertyExpression);
            var classType = PropertyHelper.GetPropertyClassName(propertyExpression);

            return (classType.GetProperty(propertyName)
                .GetCustomAttributes(true)
                .SingleOrDefault(x =>
                    x.GetType() == typeof(UpdateAttribute))
                as UpdateAttribute)
                .GetUpdateType();
        }

        // Gets metadate using reflection
        private static PropertyMetadata FetchMetadata<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyMetadata = new PropertyMetadata();
            propertyMetadata.Name = PropertyHelper.GetPropertyName(propertyExpression);
            propertyMetadata.Value = PropertyHelper.GetPropertyValue(propertyExpression);

            var classType = PropertyHelper.GetPropertyClassName(propertyExpression);
            propertyMetadata.UpdateType = (classType.GetProperty(propertyMetadata.Name)
                .GetCustomAttributes(true)
                .SingleOrDefault(x => x.GetType() == typeof(UpdateAttribute)) as UpdateAttribute)
                .GetUpdateType();

            return propertyMetadata;
        }
    }
}