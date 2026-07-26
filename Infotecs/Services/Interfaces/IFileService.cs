using Infotecs.Models;
using Infotecs.Models.Dto;
using Infotecs.Models.Requests;

namespace Infotecs.Services.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Обрабатывает полученный CSV файл
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат с Dto добавленного файла или ошибкой</returns>
    public Task<Result<FileDto>> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    /// <summary>
    /// Возвращает последние 10 записей значений по имени файла, отсортированных по времени начала
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат с найденными значениями или ошибкой</returns>
    public Task<Result<List<ValueDto>>> GetLastValues(string fileName, CancellationToken cancellationToken);
    /// <summary>
    /// Возвращает результаты вычислений по фильтрам
    /// </summary>
    /// <param name="request">Запрос с фильтрами</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат со списком Dto найденных результатов или ошибкой</returns>
    public Task<Result<List<ResultDto>>> GetResultsByRequestAsync(GetResultsRequest request, CancellationToken cancellationToken);
}