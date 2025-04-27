namespace Isatays.FTGO.OrderService.Infrastructure.Services.Options;

public class KafkaConfig
{
    public string BootstrapServers { get; set; }
    public string ClientId { get; set; }
    public int MessageTimeoutMs { get; set; }
}