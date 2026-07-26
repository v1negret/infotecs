using Infotecs.Models.Dto;

namespace Infotecs.Models.Entities;

/// <summary>
/// Сущность Строки csv файла
/// </summary>
public class ValueEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// Идентификатор файла, к которому относится строка файла
    /// </summary>
    public Guid FileUid { get; set; }
    /// <summary>
    /// Дата начала
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Время выполнения в секундах
    /// </summary>
    public int ExecutionTime { get; set; }
    /// <summary>
    /// Значение показателя
    /// </summary>
    public decimal Value { get; set; }
    
    /// <summary>
    /// Навигационное свойство связанного файла
    /// </summary>
    public FileEntity?  File { get; set; }

    /// <summary>
    /// Создать Dto объект на основе сущности
    /// </summary>
    /// <returns>Dto объект сущности</returns>
    public ValueDto ToDto() =>
        new()
        {
            Uid = Uid,
            FileUid = FileUid,
            Date = Date,
            ExecutionTime = ExecutionTime,
            Value = Value
        };
}