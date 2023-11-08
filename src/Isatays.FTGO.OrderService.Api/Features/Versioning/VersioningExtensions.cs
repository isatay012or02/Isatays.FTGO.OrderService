using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Isatays.FTGO.OrderService.Api.Features.Versioning;

/// <summary>Предоставляет метод расширения для <see cref="WebApplicationBuilder"/></summary>
public static class VersioningExtensions
{
    /// <summary>Настраивает версионирование для хоста</summary>
	public static WebApplicationBuilder ConfigureHostVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
            .AddVersionedApiExplorer(options =>
                options.ApiVersionParameterSource = new UrlSegmentApiVersionReader());

        Log.Debug("Конфигурация версионирования проекта ({Application})...", builder.Environment.ApplicationName);

        return builder;
    }
}
