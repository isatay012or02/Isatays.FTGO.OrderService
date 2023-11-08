using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MassTransit.RabbitMqTransport;

namespace Isatays.FTGO.OrderService.Infrastructure.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var host = _configuration.GetSection("RabbitMQ");
        
        var factory = new ConnectionFactory() { Uri = new Uri(host["Host"]) };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: host["QueueName"],
                       durable: false,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                       routingKey: host["QueueName"],
                       basicProperties: null,
                       body: body);
    }
}
