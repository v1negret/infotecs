using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services.Interfaces;

public interface ICsvService
{
    /// <summary>
    /// Достаёт данные из CSV файла
    /// </summary>
    /// <param name="formFile">CSV файл</param>
    /// <returns>Результат с прочитанными значениями или ошибкой</returns>
    public Task<Result<ICollection<ValueEntity>>> ReadAsync(IFormFile formFile);
}