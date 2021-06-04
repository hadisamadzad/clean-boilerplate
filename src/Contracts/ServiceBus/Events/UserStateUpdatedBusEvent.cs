namespace Contracts.ServiceBus.Events
{
    public class UserStateUpdatedBusEvent : BusEvent
    {
        public int UserId { get; set; }
        public string State { get; set; }
    }
}
