using Infotecs.Models.Requests;
using Infotecs.Services.Interfaces;
using Infotecs.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Infotecs.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    public FileController(IFileService fileService, IStringLocalizer<SharedResources> localizer)
    {
        _fileService = fileService;
        _localizer = localizer;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file.Length == 0 || !Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            return BadRequest(_localizer[SharedResources.WrongFileError].Value);
        var result = await _fileService.UploadFileAsync(file, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }

    [HttpPost("results")]
    public async Task<IActionResult> GetResults(GetResultsRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _fileService.GetResultsByRequestAsync(request, cancellationToken);
        if(!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetLastValues(string fileName, CancellationToken cancellationToken = default)
    {
        var result = await _fileService.GetLastValues(fileName, cancellationToken);
        if(!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }
}