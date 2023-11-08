namespace Isatays.FTGO.OrderService.Core.Interfaces;

public interface IRabbitMqService
{
    void SendMessage(object obj);

    void SendMessage(string message);
}
