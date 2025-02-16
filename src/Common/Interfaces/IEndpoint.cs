using Microsoft.AspNetCore.Builder;

namespace Common.Interfaces;

public interface IEndpoint
{
    void MapEndpoints(WebApplication app);
}