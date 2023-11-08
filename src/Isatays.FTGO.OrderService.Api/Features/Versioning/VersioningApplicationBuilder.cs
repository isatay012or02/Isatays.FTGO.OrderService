namespace Isatays.FTGO.OrderService.Api.Features.Versioning;

public static class VersioningApplicationBuilder
{
    /// <summary>
    /// Добавляет в пайплайн версионирование
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseConfiguredVersioning(this IApplicationBuilder app)
    {
        app.UseApiVersioning();

        return app;
    }
}
