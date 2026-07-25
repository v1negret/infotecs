using Infotecs.Models;
using Infotecs.Models.Dto;
using Infotecs.Models.Requests;

namespace Infotecs.Services.Interfaces;

public interface IFileService
{
    public Task<Result<FileDto>> UploadFileAsync(IFormFile file);
    
    public Task<Result<List<ValueDto>>> GetLastValues(string fileName);
    public Task<Result<List<ResultDto>>> GetResultsByRequestAsync(GetResultsRequest request);
}