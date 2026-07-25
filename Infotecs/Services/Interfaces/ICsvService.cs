using Infotecs.Models;
using Infotecs.Models.Entities;

namespace Infotecs.Services.Interfaces;

public interface ICsvService
{
    public Task<Result<ICollection<ValueEntity>>> ReadAsync(IFormFile formFile);
}