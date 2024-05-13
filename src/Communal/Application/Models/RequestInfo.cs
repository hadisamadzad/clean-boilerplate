namespace Communal.Application.Models;

public record RequestInfo
{
    public int? RequestedBy { get; set; }
    public string IpAddress { get; set; }
}