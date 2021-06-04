namespace Contracts.ServiceBus.Rpc.Identity
{
    public class GetUserBusRequest : BusRequest
    {
        public int UserId { get; set; }
    }
}