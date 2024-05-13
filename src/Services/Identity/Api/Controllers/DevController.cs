using Communal.Api.Infrastructure;
using Identity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class DevController(
    ILogger<DevController> logger,
    ITransactionalEmailService emailService) : ControllerBase
{
    private readonly ILogger<DevController> _logger = logger;
    private readonly ITransactionalEmailService _emailService = emailService;

    [HttpGet(Routes.Dev + "secrets/swagger")]
    public ActionResult CheckSwaggerCode([FromQuery] string key)
    {
        if (key != "foxtrot11")
            return Unauthorized(false);
        return Ok(true);
    }

    [HttpGet(Routes.Dev + "logger/test")]
    public ActionResult CheckLogger([FromQuery] string message)
    {
        _logger.LogInformation($"Hey, we have got a log: {message}");
        return Ok(true);
    }

    [HttpGet(Routes.Dev + "hashid/encode/{id}")] // api/dev/hashid/encode/id
    //[Authorize(Permission.DevelopmentAll)]
    public ActionResult GetEncodedId([FromRoute] int id)
    {
        return Ok(id.Encode());
    }

    [HttpGet(Routes.Dev + "hashid/decode/{id}")] // api/dev/hashid/decode/id
    //[Authorize(Permission.DevelopmentAll)]
    public ActionResult GetDecodeId([FromRoute] string id)
    {
        return Ok(id.Decode());
    }

    [HttpGet(Routes.Dev + "email")]
    public async Task<ActionResult> SendEmail()
    {
        var parameters = new Dictionary<string, string>
        {
            { "Link", "https://hadisamadzad.com" }
        };
        _ = await _emailService
            .SendEmailByTemplateIdAsync(1, ["h.samadzad@gmail.com"], parameters);

        return Ok("Email sent");
    }
}
