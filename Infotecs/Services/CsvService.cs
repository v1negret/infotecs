using System.Globalization;
using CsvHelper;
using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Services.Interfaces;
using Infotecs.Shared;
using Microsoft.Extensions.Localization;

namespace Infotecs.Services;

public class CsvService : ICsvService
{
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CsvService(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
    }

    public async Task<Result<ICollection<ValueEntity>>> ReadAsync(IFormFile formFile)
    {
        await using  var stream = formFile.OpenReadStream();
        var streamReader = new StreamReader(stream);
        var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        var counter = 0;
        var values = new  List<ValueEntity>();
        while (await csv.ReadAsync())
        {
            if (++counter > 10_000)
                return new Result<ICollection<ValueEntity>>(false,
                    StatusCodes.Status400BadRequest,
                    string.Format(_localizer[SharedResources.LineLimitError].Value, counter));

            var parseResult = ParseRow(csv[0], csv[1], csv[2], counter);
            if (!parseResult.IsSuccess)
                return new Result<ICollection<ValueEntity>>(false,
                    parseResult.StatusCode, parseResult.Message);

            values.Add(parseResult.Value);
            
        }
        
        if (counter == 0)
            return new Result<ICollection<ValueEntity>>(false, StatusCodes.Status400BadRequest,
                _localizer[SharedResources.LineLimitError].Value);
        
        return new Result<ICollection<ValueEntity>>(true, StatusCodes.Status200OK, values, null);
    }
    private Result<ValueEntity> ParseRow(string? dateString, string? timeString, string? valueString, int lineNumber)
    {
        if (!DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH-mm-ss.ffffZ", CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, out var date))
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.WrongDateTimeFormat].Value, lineNumber));
        if(date > DateTime.UtcNow || date.Year < 2000)
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.InvalidDateTimeRange].Value, lineNumber));
        if (!int.TryParse(timeString, out var executionTime))
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.WrongExecutionTimeFormat].Value, lineNumber));
        if(executionTime < 0)
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.InvalidExecutionTimeRange].Value, lineNumber));
        if (!decimal.TryParse(valueString, CultureInfo.InvariantCulture, out var value))
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.WrongValueFormat].Value, lineNumber));
        if(value < 0)
            return new Result<ValueEntity>(false,
                StatusCodes.Status400BadRequest,
                string.Format(_localizer[SharedResources.InvalidValueRange].Value, lineNumber));

        return new Result<ValueEntity>(new ValueEntity
        {
            Date = date,
            ExecutionTime = executionTime,
            Value = value
        });
    }
}