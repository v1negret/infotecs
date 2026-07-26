using Infotecs.Models.Dto;

namespace Infotecs.Models.Entities;

/// <summary>
/// Сущность Файла
/// </summary>
public class FileEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// Название файла
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Дата первой записи файла в БД
    /// </summary>
    public DateTime CreationTime { get; set; }
    /// <summary>
    /// Дата обновления данных файла в БД
    /// </summary>
    public DateTime UpdateTime { get; set; }
    /// <summary>
    /// Навигационное свойство для связанных с файлом значений
    /// </summary>
    public List<ValueEntity>? Values { get; set; }
    /// <summary>
    /// Навигационное свойство для связанных с файлом результатов вычислений
    /// </summary>
    public ResultEntity? Result { get; set; }
    /// <summary>
    /// Создать Dto объект на основе сущности
    /// </summary>
    /// <returns>Dto объект сущности</returns>
    public FileDto ToDto() =>
        new()
        {
            Uid = Uid,
            Name = Name,
            CreationTime = CreationTime,
            UpdateTime = UpdateTime,
            Values = Values?.Select(x => x.ToDto()).ToList(),
            Result = Result?.ToDto()
        };
}