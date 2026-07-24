using Infotecs.Data;
using Infotecs.Extensions;
using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infotecs.Repositories;

public class FileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileEntity> AddAsync(FileEntity file)
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
            
            return existingFile;
        }
        
        file.CreationTime = DateTime.UtcNow;
        
        await _dbContext.Files.AddAsync(file);
        await _dbContext.SaveChangesAsync();
        
        return file;
    }

    public async Task<List<ResultEntity>> GetFilteredResultsAsync(GetResultsRequest request)
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

        var result = await query
            .ToListAsync();

        return result;
    }

    public async Task<List<ValueEntity>> GetLastValues(string fileName)
    {
        var file = await _dbContext.Files.FirstOrDefaultAsync(f => f.Name.Equals(fileName));
        if (file is null)
            return new List<ValueEntity>();
        var values = _dbContext.Values
            .Where(v => v.FileUid.Equals(file.Uid))
            .OrderByDescending(v => v.Date)
            .Take(10)
            .AsEnumerable()
            .Reverse()
            .ToList();
        return values;
    }
}
    