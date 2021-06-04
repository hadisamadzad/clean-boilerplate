using Communal.Application.Models;
using Microsoft.AspNetCore.Http;

namespace Communal.Api.Extensions.AspNetCore
{
    public static class HttpRequestExtension
    {
        private static int GetUserId(this HttpRequest request)
        {
            //var eid = JwtHelper.GetPayload(request.GetAuthToken())?
            //    .SingleOrDefault(x => x.Key == "nameid").Value;
            //return eid?.ToString().Decode() ?? 0;
            return 0;
        }
        private static string GetIpAddress(this HttpRequest request) =>
            request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

        public static RequestInfo GetRequestInfo(this HttpRequest request) => new RequestInfo
        {
            UserId = request.GetUserId(),
            IpAddress = request.GetIpAddress(),
        };
    }
}