using System.Text;
using System.Text.Json.Serialization;
using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using Identity.Infrastructure.Brevo.Models;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure;

public class BrevoEmailService(HttpClient httpClient, IOptions<BrevoConfig> brevoConfig) :
    IEmailService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly BrevoConfig _brevoConfig = brevoConfig.Value;

    public async Task<bool> SendEmailByTemplateIdAsync(long templateId, List<string> recipients,
        Dictionary<string, string> parameters)
    {
        var request = new BrevoEmailRequest
        {
            TemplateId = templateId,
            To = recipients.ConvertAll(email => new RecipientModel { Email = email }),
            Params = parameters
        };

        var body = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _brevoConfig.ApiKey);

        var response = await _httpClient.PostAsync(_brevoConfig.SendEmailUri,
            new StringContent(body, Encoding.UTF8, "application/json"));

        return response.IsSuccessStatusCode;
    }
}