using System.Reflection;
using Common.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Common.Extensions;

public static class EndpointWebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var interfaceType = typeof(IEndpoint);
        var method = interfaceType.GetMethods().Single();

        var endpointClasses = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => interfaceType.IsAssignableFrom(x) &&
                x.IsClass && !x.IsAbstract);

        foreach (var endpointClass in endpointClasses)
        {
            var instance = Activator.CreateInstance(endpointClass);
            method.Invoke(instance, [app]);
        }
    }
}