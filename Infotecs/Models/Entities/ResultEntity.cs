using Infotecs.Models.Dto;

namespace Infotecs.Models.Entities;

/// <summary>
/// Сущность результата вычислений
/// </summary>
public class ResultEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// Идентификатор файла, к которому относятся вычисления
    /// </summary>
    public Guid FileUid { get; set; }
    /// <summary>
    /// Дельта времени(MaxDate-MinDate) в секундах
    /// </summary>
    public int DateTimeDelta { get; set; }
    /// <summary>
    /// Минимальная дата и время, как момент запуска первой операции
    /// </summary>
    public DateTime MinDateTime { get; set; }
    /// <summary>
    /// Среднее время выполнения
    /// </summary>
    public double AvgExecutionTime { get; set; }
    /// <summary>
    /// Среднее значение по показателям
    /// </summary>
    public decimal AvgValue { get; set; }
    /// <summary>
    /// Медиана по показателям
    /// </summary>
    public decimal MedianValue { get; set; }
    /// <summary>
    /// Минимальное значение показателя
    /// </summary>
    public decimal MinValue { get; set; }
    /// <summary>
    /// Максимальное значение показателя
    /// </summary>
    public decimal MaxValue { get; set; }
    /// <summary>
    /// Навигационное свойство связанного файла
    /// </summary>
    public FileEntity? File { get; set; }

    /// <summary>
    /// Создать Dto объект на основе сущности
    /// </summary>
    /// <returns>Dto объект сущности</returns>
    public ResultDto ToDto() =>
        new()
        {
            Uid = Uid,
            FileUid = FileUid,
            DateTimeDelta = DateTimeDelta,
            MinDateTime = MinDateTime,
            AvgExecutionTime = AvgExecutionTime,
            AvgValue = AvgValue,
            MedianValue = MedianValue,
            MinValue = MinValue,
            MaxValue = MaxValue,
        };
}