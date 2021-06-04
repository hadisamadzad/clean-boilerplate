namespace Identity.Application.Infrastructure.PartialUpdates
{
    public class PartialUpdatePayload
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public UpdateType UpdateType { get; set; }
    }
}