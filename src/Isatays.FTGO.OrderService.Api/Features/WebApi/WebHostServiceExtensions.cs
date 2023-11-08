using Isatays.FTGO.OrderService.Api.Common.Constants;
using Isatays.FTGO.OrderService.Api.Features.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Isatays.FTGO.OrderService.Api.Features.WebApi;

public static class WebHostServiceExtensions
{
    /// <summary>
	/// Добавляет расширение для веб хоста
	/// </summary>
	/// <param name="builder"></param>
	/// <returns></returns>
	public static WebApplicationBuilder ConfigureWebHost(this WebApplicationBuilder builder)
    {
        if (builder.Environment.EnvironmentName.Equals(EnvironmentConstant.Test))
        {
            builder.WebHost.UseUrls(builder.Configuration["WebHostOptions:WebAddress"]);
        }

        builder.Services.AddControllers()
               .AddNewtonsoftJson(x =>
               {
                   x.SerializerSettings.Converters.Add(new StringEnumConverter());
                   x.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                   x.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                   x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                   x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });
        builder.Services.AddHealthChecks();
        builder.Services.AddConfiguredSwagger();

        return builder;
    }
}
