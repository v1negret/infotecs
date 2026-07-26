using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services.Interfaces;

public interface ITimescaleDataAggregator
{
    /// <summary>
    /// Обрабатывает значения показателя
    /// </summary>
    /// <param name="values">Значение показателя</param>
    /// <returns>Результат вычисления</returns>
    public Result<ResultEntity> Aggregate(ICollection<ValueEntity> values);
}