using Microsoft.AspNetCore.Http;

namespace Communal.Api.Extensions.AspNetCore;

public static class HttpRequestExtension
{
    private static string GetIpAddress(this HttpRequest request) =>
        request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
}