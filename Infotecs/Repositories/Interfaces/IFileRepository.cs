using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;

namespace Infotecs.Repositories.Interfaces;

public interface IFileRepository
{
    public Task<Result<FileEntity>> AddAsync(FileEntity file);

    public Task<Result<List<ResultEntity>>> GetFilteredResultsAsync(GetResultsRequest request);

    public Task<Result<List<ValueEntity>>> GetLastValues(string fileName);
}