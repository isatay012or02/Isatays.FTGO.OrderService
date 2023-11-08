using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Isatays.FTGO.OrderService.Api.Features.Swagger;

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly ILogger<SwaggerConfigureOptions> _logger;
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Создает экземпляр <see cref="SwaggerConfigureOptions"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="provider"></param>
    public SwaggerConfigureOptions(ILogger<SwaggerConfigureOptions> logger, IApiVersionDescriptionProvider provider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            try
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Добавляет в сваггер дополнительную информацию о компании, лицензии и тд.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Web Api для Описание еды",
            Version = GetAssemblyVersion(),
            Description = GetSwaggerDescription(),
            License = new OpenApiLicense
            {
                Name = "Лицензия",
                Url = new Uri(
                    "https://hcsbk.kz/%D0%9B%D0%B8%D1%86%D0%B5%D0%BD%D0%B7%D0%B8%D1%8F%20%D0%BE%D1%82%2020.04.2021%20%D0%B3%D0%BE%D0%B4%D0%B0.pdf"),
            },
        };

        if (description.IsDeprecated)
        {
            info.Description += " Версия API не поддерживается.";
        }

        return info;
    }

    /// <summary>
    /// Получает нужную информацию о версии сборки проекта из ассембли проекта
    /// </summary>
    /// <returns></returns>
    private static string GetAssemblyVersion()
    {
        return Assembly.GetEntryAssembly()
                      ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                      ?.InformationalVersion ?? "undefined";
    }

    /// <summary>
    /// Получает информацию из xml для отображение в визуальном интерфейсе сваггер
    /// </summary>
    /// <returns></returns>
    private string GetSwaggerDescription()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "SwaggerDescription.xml");

        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception)
        {
            return String.Empty;
        }
    }
}
