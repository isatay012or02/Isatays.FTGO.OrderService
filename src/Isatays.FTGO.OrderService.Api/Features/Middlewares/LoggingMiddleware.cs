using Isatays.FTGO.OrderService.Api.Common.Constants;

namespace Isatays.FTGO.OrderService.Api.Features.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>Создает экземпляр <see cref="LoggingMiddleware"/></summary>
    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _loggerFactory = loggerFactory;
    }

    /// <summary>Промежуточный метод</summary>
    public async Task InvokeAsync(HttpContext context)
    {
        var logger = _loggerFactory.CreateLogger<LoggingMiddleware>();

        var scope = new Dictionary<string, object>
        {
            { LogKey.RequestedWith, context.Request.Headers[HeaderConstant.RequestedWith].ToString() },
        };

        using (logger.BeginScope(scope))
        {
            await _next(context);
        }
    }
}
