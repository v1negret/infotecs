using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services.Interfaces;

public interface ITimescaleDataAggregator
{
    public Result<ResultEntity> Aggregate(ICollection<ValueEntity> values);
}