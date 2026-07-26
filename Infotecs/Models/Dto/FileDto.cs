namespace Infotecs.Models.Dto;

public class FileDto
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
    /// Список значений показателя файла
    /// </summary>
    public List<ValueDto>? Values { get; set; }
    /// <summary>
    /// Результат вычислений по показателям файла
    /// </summary>
    public ResultDto? Result { get; set; }
}