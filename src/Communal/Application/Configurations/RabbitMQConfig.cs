namespace Communal.Application.Configurations
{
    public class RabbitMQConfig
    {
        public const string Key = "RabbitMQ";

        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Nodes { get; set; }
    }
}
