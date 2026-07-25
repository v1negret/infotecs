using Infotecs.Data;
using Infotecs.Extensions;
using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;
using Infotecs.Repositories.Interfaces;
using Infotecs.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Infotecs.Repositories;

public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public FileRepository(ApplicationDbContext dbContext, IStringLocalizer<SharedResources> localizer)
    {
        _dbContext = dbContext;
        _localizer = localizer;
    }

    public async Task<Result<FileEntity>> AddAsync(FileEntity file)
    {
        var existingFile = _dbContext.Files
            .Include(f => f.Values)
            .Include(f => f.Result)
            .FirstOrDefault(x => x.Name.Equals(file.Name));
        
        if (existingFile is not null)
        {
            if (existingFile.Result is not null) 
                _dbContext.Remove(existingFile.Result);
            _dbContext.RemoveRange(existingFile.Values);
            
            existingFile.Result = file.Result;
            existingFile.Values = file.Values;
            existingFile.UpdateTime = DateTime.UtcNow;
            
            await _dbContext.SaveChangesAsync();
            return new Result<FileEntity>(true, StatusCodes.Status200OK, existingFile, null);
        }
        
        file.CreationTime = DateTime.UtcNow;
        
        await _dbContext.Files.AddAsync(file);
        await _dbContext.SaveChangesAsync();
        
        return new Result<FileEntity>(true, StatusCodes.Status200OK, file, null);
    }

    public async Task<Result<List<ResultEntity>>> GetFilteredResultsAsync(GetResultsRequest request)
    {
        var query = _dbContext.Results.AsNoTracking().AsQueryable();

        query = query
            .WhereIf(!string.IsNullOrEmpty(request.FileName),
                r => r.File != null && r.File.Name.Contains(request.FileName!))
            .WhereIf(request.MinStartTime.HasValue,
                r => r.MinDateTime >= request.MinStartTime!.Value)
            .WhereIf(request.MaxStartTime.HasValue,
                r => r.MinDateTime <= request.MaxStartTime!.Value)
            .WhereIf(request.MinAvgValue.HasValue,
                r => r.AvgValue >= request.MinAvgValue!.Value)
            .WhereIf(request.MaxAvgValue.HasValue,
                r => r.AvgValue <= request.MaxAvgValue!.Value)
            .WhereIf(request.MinExecTime.HasValue,
                r => r.AvgExecutionTime >= request.MinExecTime!.Value)
            .WhereIf(request.MaxExecTime.HasValue,
                r => r.AvgExecutionTime <= request.MaxExecTime!.Value);
        
        var values = await query
            .ToListAsync();

        return new Result<List<ResultEntity>>(true, StatusCodes.Status200OK, values, null);
    }

    public async Task<Result<List<ValueEntity>>> GetLastValues(string fileName)
    {
        var file = await _dbContext.Files.FirstOrDefaultAsync(f => f.Name.Equals(fileName));
        if (file is null)
            return new Result<List<ValueEntity>>(false, StatusCodes.Status404NotFound, _localizer[SharedResources.FileNotFound].Value);
        
        var values = _dbContext.Values
            .Where(v => v.FileUid.Equals(file.Uid))
            .OrderByDescending(v => v.Date)
            .Take(10)
            .AsEnumerable()
            .Reverse()
            .ToList();
        
        if (values.Count == 0)
            return new Result<List<ValueEntity>>(false, StatusCodes.Status404NotFound, _localizer[SharedResources.LastValuesNotFound].Value);
        
        return new Result<List<ValueEntity>>(true, StatusCodes.Status200OK, values, null);
    }
}
    