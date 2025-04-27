using System.Text.Json;
using Confluent.Kafka;
using Isatays.FTGO.OrderService.Core.Application.Events;
using Isatays.FTGO.OrderService.Core.Ports;
using Microsoft.Extensions.Logging;

namespace Isatays.FTGO.OrderService.Infrastructure.Services;

public class KafkaEventProducer : IEventProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventProducer> _logger;

    public KafkaEventProducer(IProducer<string, string> producer, ILogger<KafkaEventProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task ProduceAsync<T>(string topic, T eventMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            var serializedMessage = JsonSerializer.Serialize(eventMessage);
            
            var messageKey = "unknown-key";
            if (eventMessage is OrderCreatedEvent orderEvent)
                messageKey = orderEvent.OrderId.ToString();

            var message = new Message<string, string>
            {
                Key = messageKey,
                Value = serializedMessage
            };

            var deliveryResult = await _producer.ProduceAsync(topic, message, cancellationToken);
                
            _logger.LogInformation(
                "Delivered message to {Topic} [{Partition}] at offset {Offset}",
                deliveryResult.Topic, deliveryResult.Partition, deliveryResult.Offset);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error producing message to Kafka");
            throw;
        }
    }
}