using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;

namespace Infotecs.Repositories.Interfaces;

public interface IFileRepository
{
    public Task<Result<FileEntity>> AddAsync(FileEntity file, CancellationToken cancellationToken);

    public Task<Result<List<ResultEntity>>> GetFilteredResultsAsync(GetResultsRequest request, CancellationToken cancellationToken);

    public Task<Result<List<ValueEntity>>> GetLastValues(string fileName, CancellationToken cancellationToken);
}