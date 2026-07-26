using Infotecs.Models;
using Infotecs.Models.Entities;
using Infotecs.Models.Requests;

namespace Infotecs.Repositories.Interfaces;

public interface IFileRepository
{
    /// <summary>
    /// Добавление данных о файле и его содержимом в БД
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат с добавленным файлом или ошибкой</returns>
    public Task<Result<FileEntity>> AddAsync(FileEntity file, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает результаты вычислений по фильтрам
    /// </summary>
    /// <param name="request">Запрос с фильтрами</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат с найденными результатами или ошибкой</returns>
    public Task<Result<List<ResultEntity>>> GetFilteredResultsAsync(GetResultsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает последние 10 записей значений по имени файла, отсортированных по времени начала
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат с найденными значениями или ошибкой</returns>
    public Task<Result<List<ValueEntity>>> GetLastValues(string fileName, CancellationToken cancellationToken);
}