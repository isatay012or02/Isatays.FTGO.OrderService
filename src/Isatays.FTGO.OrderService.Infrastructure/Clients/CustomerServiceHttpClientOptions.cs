namespace Isatays.FTGO.OrderService.Infrastructure.Clients;

public class CustomerServiceHttpClientOptions
{
    public const string SectionName = nameof(CustomerServiceHttpClientOptions);

    public string Url { get; init; } = string.Empty;

    public string EndpointToVerifyCustomer { get; init; } = string.Empty;

    public int TimeoutInSeconds { get; init; } = 30;
    public int RetryCount { get; init; } = 3;
    public int[] RetryDelaysInSeconds { get; init; } = new int[] { 1, 5, 10, };
    public int[] StatusCodesToRetry { get; init; } = new int[] { 408, 500 };
}
