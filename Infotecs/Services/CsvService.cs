using System.Globalization;
using CsvHelper;
using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services;

public class CsvService
{
    public async Task<IEnumerable<ValueEntity>> ReadAsync(IFormFile formFile)
    {
        var result = new  List<ValueEntity>();
        await using  var stream = formFile.OpenReadStream();
        var streamReader = new StreamReader(stream);
        var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        while (await csv.ReadAsync())
        {
            var dateString = csv[0];
            var executionTimeString = csv[1];
            var valueString =  csv[2];
            
            if(!DateTime.TryParseExact(dateString,"yyyy-MM-ddTHH-mm-ss.ffffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date))
                throw new ArgumentException();
            if (!int.TryParse(executionTimeString, out var executionTime))
                throw new ArgumentException();
            if (!decimal.TryParse(valueString, CultureInfo.InvariantCulture, out var value))
                throw new ArgumentException();
            
            result.Add(new()
            {
                Date = date,
                ExecutionTime = executionTime,
                Value = value,
            });
        }
        
        return result;
    }
}