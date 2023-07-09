using System.Net;
using interview.test.ensek.Core.Contracts;
using interview.test.ensek.Core.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.test.ensek.Host.WebApi.Controllers;

[ApiController]
[Route("api/")]
public class MeterReadingsImporterController : ControllerBase
{
    private readonly IMeterReadingsImporter _importer;
    private readonly ILogger<MeterReadingsImporterController> _logger;


    public MeterReadingsImporterController(
        ILogger<MeterReadingsImporterController> logger,
        IMeterReadingsImporter importer)
    {
        _logger = logger;
        _importer = importer;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("meter-reading-uploads")]
    [ProducesResponseType(typeof(ImportResultDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Import(IFormFile source, CancellationToken cancellationToken)
    {
        if(source is null || source.Length == 0)
        {
            return BadRequest();
        }
        
        using var ms = new MemoryStream();

        await source.CopyToAsync(ms, cancellationToken);
        ms.Position = 0;

        var result = await _importer.Import(ms, cancellationToken);

        return Ok(result);
    }
}
