namespace Isatays.FTGO.OrderService.Infrastructure.Clients;

public class AccountServiceHttpClientOptions
{
    public const string SectionName = nameof(AccountServiceHttpClientOptions);

    public string Url { get; init; } = string.Empty;

    public string EndpointToAuthorizeCreditCard { get; init; } = string.Empty;

    public int TimeoutInSeconds { get; init; } = 30;
    public int RetryCount { get; init; } = 3;
    public int[] RetryDelaysInSeconds { get; init; } = new int[] { 1, 5, 10, };
    public int[] StatusCodesToRetry { get; init; } = new int[] { 408, 500 };
}
