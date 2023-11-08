using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Isatays.FTGO.OrderService.Api.Features.Swagger;

/// <summary>
/// Расширение для интерфейса IApplicationBuilder
/// </summary>
public static class SwaggerApplicationBuilder
{
    /// <summary>
    /// Встраивает в пайплайн визуальный интерфейс для сваггер и добавляет работу с версионированием
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices
                                               .GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseDeveloperExceptionPage();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());

                options.RoutePrefix = "swagger";
            }
        });

        return app;
    }
}
