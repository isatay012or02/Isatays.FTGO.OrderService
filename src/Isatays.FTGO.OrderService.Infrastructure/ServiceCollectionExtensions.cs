using Isatays.FTGO.OrderService.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Isatays.FTGO.OrderService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Isatays.FTGO.OrderService.Core.Orders;
using Isatays.FTGO.OrderService.Infrastructure.Services;
using MassTransit;
using Isatays.FTGO.OrderService.Infrastructure.Clients;
using MassTransit.EntityFrameworkCoreIntegration;

namespace Isatays.FTGO.OrderService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration, string environmentName)
    {
        var connectionString = configuration.GetConnectionString("Ftgo")!;

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        3,
                        TimeSpan.FromSeconds(10),
                        null);
                });
        });

        return services;
    }

    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.AddScoped<IDataContext, DataContext>();

        services.AddScoped<CustomerServiceHttpClient>();
        services.AddHttpClient(nameof(CustomerServiceHttpClient));

        return services;
    }

    public static IServiceCollection ConfigureInfrastructureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                    r.AddDbContext<DbContext, SagaDbContext>((provider, options) =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    });
                });

            x.AddConsumer<CreateOrderCommandHandler>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration[""], h =>
                {
                    h.Username(configuration[""]!);
                    h.Password(configuration[""]!);
                });

                cfg.ReceiveEndpoint("order_saga_queue", e =>
                {
                    e.ConfigureSaga<OrderState>(context);
                    e.ConfigureConsumer<CreateOrderCommandHandler>(context);
                });
            });
        });
        
        
        services.AddMassTransitHostedService();
        
        return services;
    }

    public static IServiceCollection ConfigureInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomerServiceHttpClientOptions>(
            configuration.GetSection(CustomerServiceHttpClientOptions.SectionName));

        return services;
    }
}
