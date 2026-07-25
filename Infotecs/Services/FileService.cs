using Infotecs.Models;
using Infotecs.Models.Dto;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;
using Infotecs.Repositories.Interfaces;
using Infotecs.Services.Interfaces;

namespace Infotecs.Services;

public class FileService : IFileService
{
    private readonly ICsvService _csvService;
    private readonly ITimescaleDataAggregator _timescaleDataAggregator;
    private readonly IFileRepository _fileRepository;

    public FileService(ICsvService csvService, ITimescaleDataAggregator timescaleDataAggregator,
        IFileRepository fileRepository)
    {
        _csvService = csvService;
        _timescaleDataAggregator = timescaleDataAggregator;
        _fileRepository = fileRepository;
    }

    public async Task<Result<FileDto>> UploadFileAsync(IFormFile file)
    {
        var fileEntity = new FileEntity
        {
            Name = file.FileName,
        };
        var values = (await _csvService.ReadAsync(file));
        if (!values.IsSuccess)
            return new Result<FileDto>(values.IsSuccess, values.StatusCode, values.Message);
        
        var result =  _timescaleDataAggregator.Aggregate(values.Value);
        if(!result.IsSuccess)
            return new Result<FileDto>(result.IsSuccess, result.StatusCode, result.Message);
        fileEntity.Values = values.Value.ToList();
        fileEntity.Result = result.Value;
        
        var addedFile = await _fileRepository.AddAsync(fileEntity);
        if (!addedFile.IsSuccess)
            return new Result<FileDto>(addedFile.IsSuccess, addedFile.StatusCode, addedFile.Message);
        
        return new Result<FileDto>(addedFile.IsSuccess, addedFile.StatusCode, addedFile.Value.ToDto(), addedFile.Message);
    }

    public async Task<Result<List<ResultDto>>> GetResultsByRequestAsync(GetResultsRequest request)
    {
        var filteredResults = await _fileRepository.GetFilteredResultsAsync(request);
        if (!filteredResults.IsSuccess)
            return new Result<List<ResultDto>>(filteredResults.IsSuccess, filteredResults.StatusCode, filteredResults.Message);
        
        var resultFilteredResults = filteredResults.Value.Select(v => v.ToDto()).ToList();
        
        return new Result<List<ResultDto>>(filteredResults.IsSuccess, filteredResults.StatusCode, resultFilteredResults, filteredResults.Message);
    }

    public async Task<Result<List<ValueDto>>> GetLastValues(string fileName)
    {
        var values = await _fileRepository.GetLastValues(fileName);
        if (!values.IsSuccess)
            return new Result<List<ValueDto>>(values.IsSuccess, values.StatusCode, values.Message);
        
        var resultValuest = values.Value.Select(v => v.ToDto()).ToList();
        return new Result<List<ValueDto>>(values.IsSuccess, values.StatusCode,resultValuest, values.Message);
    }
}