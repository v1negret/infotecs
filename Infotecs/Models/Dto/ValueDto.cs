namespace Infotecs.Models.Dto;

public class ValueDto
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
    /// Время выполнения
    /// </summary>
    public int ExecutionTime { get; set; }
    /// <summary>
    /// Значение показателя
    /// </summary>
    public decimal Value { get; set; }
}