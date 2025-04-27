using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Isatays.FTGO.OrderService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Isatays.FTGO.OrderService.Core.Ports;
using Isatays.FTGO.OrderService.Infrastructure.Services;
using Isatays.FTGO.OrderService.Infrastructure.Services.Options;

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
        services.AddScoped<IDataContext, DataContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection ConfigureInfrastructureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaConfig = configuration.GetSection("KafkaConfig").Get<KafkaConfig>();
        services.AddSingleton<IProducer<string, string>>(provider =>
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaConfig.BootstrapServers,
                ClientId = kafkaConfig.ClientId,
                Acks = Acks.All,
                EnableIdempotence = true,
                MessageTimeoutMs = kafkaConfig.MessageTimeoutMs,
                RetryBackoffMs = 500,
                MaxInFlight = 1,
                EnableDeliveryReports = true
            };

            return new ProducerBuilder<string, string>(config).Build();
        });
        
        return services;
    }
}
