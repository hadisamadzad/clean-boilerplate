using System.Collections.Generic;
using System.Linq;
using Identity.Application.Infrastructure.PropertyMetadata;

namespace Identity.Application.Infrastructure.PartialUpdates
{
    public static class PartialUpdateHelper
    {
        // Removing properties with null value and also "List<SelectListItems>" properties
        public static IEnumerable<PartialUpdatePayload> GetUpdatedProperties<T>(this T obj) where T : IPartialUpdate
        {
            return obj
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetUpdateType() != UpdateType.DontUpdate)
                .Where(prop => prop.GetGetMethod() != null)
                .Where(prop => prop.GetValue(obj) != null)
                // .Where(prop => !(
                //     prop.GetValue(obj).GetType().GenericTypeArguments.ToList().Count != 0 &&
                //     prop.GetValue(obj).GetType().GenericTypeArguments.ToList()[0].Name == typeof(SelectListItem).Name))
                .Select(prop => new PartialUpdatePayload
                {
                    Name = prop.Name, // It's an exception
                    Value = prop.GetValue(obj),
                    UpdateType = prop.GetUpdateType()
                });
        }

        // Get partial update of properties based on requested properties
        public static bool ContainsImpassableUpdate(this IEnumerable<PartialUpdatePayload> updates, params UpdateType[] impassibleTypes)
        {
            return updates.Any(x =>
                x.UpdateType == UpdateType.DontUpdate ||
                impassibleTypes.Contains(x.UpdateType));
        }

        // Get partial update of properties
        public static T UpdatePartially<T>(this T obj, IEnumerable<PartialUpdatePayload> updates) where T : IPartialUpdate
        {
            foreach (var update in updates)
            {
                // Protects dont update property
                if (update.UpdateType == UpdateType.DontUpdate)
                    continue;

                var prop = obj.GetType()
                    .GetProperties()
                    .Where(pi => pi.GetSetMethod() != null)
                    .SingleOrDefault(x => x.Name == update.Name);

                prop?.SetValue(obj, update.Value);
            }

            return obj;
        }
    }
}