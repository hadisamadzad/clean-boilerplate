using Identity.Application.Infrastructure.PartialUpdates;

namespace Identity.Application.Infrastructure.PropertyMetadata
{
    public class PropertyMetadata
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public UpdateType UpdateType { get; set; }
    }
}
