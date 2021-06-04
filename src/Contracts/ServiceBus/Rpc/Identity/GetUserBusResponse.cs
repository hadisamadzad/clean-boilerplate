using Contracts.ServiceBus.Rpc.Identity.Models;

namespace Contracts.ServiceBus.Rpc.Identity
{
    public class GetUserBusResponse : BusResponse
    {
        public UserBusModel User { get; set; }
        public enum Errors
        {
            NotFound = 1
        }
    }
}