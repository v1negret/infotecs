using Infotecs.Models;
using Infotecs.Models.Requests;
using Infotecs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Infotecs.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    public FileController(FileService fileService, IStringLocalizer<SharedResources> localizer)
    {
        _fileService = fileService;
        _localizer = localizer;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file.Length == 0 || Path.GetExtension(file.FileName).Equals("csv"))
            return BadRequest(_localizer[SharedResources.WrongFileError].Value);
        var result = await _fileService.UploadFileAsync(file);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }

    [HttpPost("results")]
    public async Task<IActionResult> GetResults(GetResultsRequest request)
    {
        var result = await _fileService.GetResultsByRequestAsync(request);
        if(!result.IsSuccess)
            StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetLastValues(string fileName)
    {
        var result = await _fileService.GetLastValues(fileName);
        if(!result.IsSuccess)
            StatusCode(result.StatusCode, result.Message);
        
        return Ok(result.Value);
    }
}