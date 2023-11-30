
using Isatays.FTGO.OrderService.Core.DTO;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Infrastructure.Common.Exceptions;
using Isatays.FTGO.OrderService.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Isatays.FTGO.OrderService.Infrastructure.Clients;

public class AccountServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AccountServiceHttpClientOptions _options;

    public AccountServiceHttpClient(IHttpClientFactory clientFactory,
        IOptions<AccountServiceHttpClientOptions> options)
    {
        _options = options.Value;
        _httpClient = clientFactory.CreateClient(nameof(AccountServiceHttpClient));
        _httpClient.BaseAddress = new Uri(_options.Url);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutInSeconds);
    }

    public async Task<Order> AuthorizeCreditCard(CreateTicketDto request)
    {
        HttpResponseMessage httpResponse;
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        try
        {
            httpResponse = await _httpClient.PostAsync(_options.EndpointToAuthorizeCreditCard, httpContent);
        }
        catch (Exception ex)
        {
            throw new RetryableException(ex.Message);
        }

        if (httpResponse.ShouldRetry(_options.StatusCodesToRetry))
            throw new RetryableException($"PostAsync({_options.EndpointToAuthorizeCreditCard})");

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Order>(content);

        return response;
    }
}
