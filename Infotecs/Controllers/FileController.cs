using Infotecs.Models.Requests;
using Infotecs.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infotecs.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    public FileController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file == null || file.Length == 0 || Path.GetExtension(file.FileName).Equals("csv"))
            return BadRequest();
        var result = await _fileService.UploadFileAsync(file);
        
        return Ok(result);
    }

    [HttpPost("results")]
    public async Task<IActionResult> GetResults(GetResultsRequest request)
    {
        var result = await _fileService.GetResultsByRequestAsync(request);
        return Ok(result);
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetLastValues(string fileName)
    {
        var result = await _fileService.GetLastValues(fileName);
        return Ok(result);
    }
}