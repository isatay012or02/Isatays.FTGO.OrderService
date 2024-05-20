using System.Reflection;

namespace Isatays.FTGO.OrderService.Api.Features.AutoMapper;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddConfiguredAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}