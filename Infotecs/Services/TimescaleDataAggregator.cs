using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services;

public class TimescaleDataAggregator
{
    public ResultEntity Aggregate(ICollection<ValueEntity> values)
    {
        var dateTimeDelta = GetDateTimeDelta(values);
        var minDateTime = values.Min(v => v.Date).Date;
        var avgExecTime = values.Average(v => v.ExecutionTime);
        var avgValue = values.Average(v => v.Value);
        var medianValue = GetMedianValue(values.Select(x => x.Value).ToList());
        var maxValue = values.Max(v => v.Value);
        var minValue = values.Min(v => v.Value);

        return new ResultEntity()
        {
            DateTimeDelta = dateTimeDelta,
            MinDateTime = minDateTime,
            AvgExecutionTime = avgExecTime,
            AvgValue = avgValue,
            MedianValue = medianValue,
            MaxValue = maxValue,
            MinValue = minValue,
        };
    }

    private int GetDateTimeDelta(ICollection<ValueEntity> values) 
        => (values.Max(v => v.Date).Date -
            values.Min(v => v.Date).Date)
            .Seconds;

    private decimal GetMedianValue(ICollection<decimal> values)
    {
        var count = values.Count();
        var orderedValues = values.Order().ToList();
        decimal result;
        if (orderedValues.Count % 2 != 0)
            result = (orderedValues[count / 2 - 1] + orderedValues[count / 2]) / 2.00m;
        else
            result = orderedValues[count / 2];
        
        return result;

    }
    
}