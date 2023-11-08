using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Isatays.FTGO.OrderService.Api.Features.Swagger;

/// <summary>
/// Расширение сваггер для добавления в сборку
/// Setup and add swagger
/// </summary>
public static class SwaggerServiceExtensions
{
    /// <summary>
    /// Добавляет сваггер в сборку проекта
    /// Добавляет в контейнер зависимостей <see cref="SwaggerConfigureOptions"/>
    /// Добавляет xml для чтения комментарией для отображение в интерфейсе
    /// Добавляет поддержку Json
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

        services.AddSwaggerGen(/*options => { options.AddXmlComment(typeof(BaseController).Assembly); }*/);

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    /// <summary>
    /// Расширяет <see cref="SwaggerGenOptions"/> добавлением комментариев из xml
    /// </summary>
    /// <param name="options"></param>
    /// <param name="assembly"></param>
    private static void AddXmlComment(this SwaggerGenOptions options, Assembly assembly)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}
