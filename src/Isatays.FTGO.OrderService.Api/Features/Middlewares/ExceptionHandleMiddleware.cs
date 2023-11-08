using Isatays.FTGO.OrderService.Api.Common.Extensions;
using Isatays.FTGO.OrderService.Core.Common.Exceptions;
using System.Net;

namespace Isatays.FTGO.OrderService.Api.Features.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly ILogger<ExceptionHandleMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>Добавляет зависимости <see cref="RequestDelegate"/> и <see cref="ILogger"/></summary>
    public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>Добавляет метод перехвата исключений в цепочке вызовов</summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RequestValidationException ex)
        {
            _logger.LogError(ex, "{message}", "Невалидные параметры запроса");

            var problem = ex.GenerateProblemDetails(context,
                "Невалидные параметры запроса",
                HttpStatusCode.BadRequest);

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", "Необработанное исключение в сервисе OrderService");

            var problem = ex.GenerateProblemDetails(context,
                "Необработанное исключение в сервисе OrderService",
                HttpStatusCode.InternalServerError);

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
