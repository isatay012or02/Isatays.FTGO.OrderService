using Serilog;
using System.Reflection;
using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Features.WebApi;

public static class WebApiExtensions
{
    /// <summary>Расширение настраивает работу контроллеров веб апи</summary>
	public static WebApplicationBuilder ConfigureWebApiControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                opt.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

        Log.Debug("Конфигурация контроллеров проекта ({Application})...", builder.Environment.ApplicationName);

        return builder;
    }

    /// <summary>Расширение настраивает провайдеры необходимые для работы веб апи</summary>
    public static WebApplicationBuilder ConfigureWebApiProviders(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        Log.Debug("Конфигурация сторонних инструментов проекта ({Application})...",
            builder.Environment.ApplicationName);

        return builder;
    }
}
