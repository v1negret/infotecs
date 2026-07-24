using Infotecs.Models.Dto;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;
using Infotecs.Repositories;

namespace Infotecs.Services;

public class FileService
{
    private readonly CsvService _csvService;
    private readonly TimescaleDataAggregator _timescaleDataAggregator;
    private readonly FileRepository _fileRepository;

    public FileService(CsvService csvService, TimescaleDataAggregator timescaleDataAggregator,
        FileRepository fileRepository)
    {
        _csvService = csvService;
        _timescaleDataAggregator = timescaleDataAggregator;
        _fileRepository = fileRepository;
    }

    public async Task<FileDto> UploadFileAsync(IFormFile file)
    {
        var fileEntity = new FileEntity
        {
            Name = file.Name,
        };
        var values = (await _csvService.ReadAsync(file)).ToList();
        var results =  _timescaleDataAggregator.Aggregate(values);
        fileEntity.Values = values;
        fileEntity.Result = results;
        var addedFile = await _fileRepository.AddAsync(fileEntity);
        
        return addedFile.ToDto();
    }

    public async Task<List<ResultDto>> GetResultsByRequestAsync(GetResultsRequest request) =>
        (await _fileRepository.GetFilteredResultsAsync(request))
        .Select(r => r.ToDto())
        .ToList();

    public async Task<List<ValueDto>> GetLastValues(string fileName) => 
        (await _fileRepository.GetLastValues(fileName)).Select(v => v.ToDto())
        .ToList();
}