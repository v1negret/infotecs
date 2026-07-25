using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;

namespace Infotecs.Shared;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger, IStringLocalizer<SharedResources> localizer) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Непредвиденная ошибка: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = localizer[SharedResources.InternalServerError].Value,
                Status = StatusCodes.Status500InternalServerError,
                Detail = localizer[SharedResources.InternalServerErrorDetails].Value,
            }
        });
    }
}
