namespace Isatays.FTGO.OrderService.Infrastructure.Common;

public static class HttpResponseValidator
{
    public static bool ShouldRetry(this HttpResponseMessage httpResponse,
        IEnumerable<int> retryStatusCodes)
    {
        return httpResponse is null ||
            (!httpResponse.IsSuccessStatusCode
                && retryStatusCodes.Contains((int)httpResponse.StatusCode));
    }
}
