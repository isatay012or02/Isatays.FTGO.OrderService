namespace Isatays.FTGO.OrderService.Core.Ports;

public interface IEventProducer
{
    Task ProduceAsync<T>(string topic, T eventMessage, CancellationToken cancellationToken = default);
}