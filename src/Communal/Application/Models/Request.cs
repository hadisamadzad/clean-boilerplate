namespace Communal.Application.Models
{
    public class Request
    {
        private int userId { get; }
        private string ipAddress { get; }

        public Request()
        {
        }

        public Request(RequestInfo info)
        {
            userId = info.UserId;
            ipAddress = info.IpAddress;
        }

        public int UserId => userId;
        public string IpAddress => ipAddress;
    }
}