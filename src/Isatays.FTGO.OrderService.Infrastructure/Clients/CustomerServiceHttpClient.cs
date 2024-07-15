using Isatays.FTGO.OrderService.Core.DTO;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Infrastructure.Common;
using Isatays.FTGO.OrderService.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Isatays.FTGO.OrderService.Core.Common.Constants;
using Isatays.FTGO.OrderService.Infrastructure.Common.Constants;
using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Infrastructure.Clients;

public class CustomerServiceHttpClient
{
    private readonly HttpClient _httpClient;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CustomerServiceHttpClientOptions _options;

    public CustomerServiceHttpClient(IHttpClientFactory clientFactory,
        IOptions<CustomerServiceHttpClientOptions> options)
    {
        //_httpContextAccessor = httpContextAccessor ??
        //        throw new ArgumentNullException(nameof(httpContextAccessor));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _httpClient = clientFactory.CreateClient(nameof(CustomerServiceHttpClient)) ??
            throw new ArgumentNullException(nameof(clientFactory));
        _httpClient.BaseAddress = new Uri(_options.Url);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutInSeconds);
    }

    public async Task<Result<bool>> VerifyCustomer(Customer request)
    {
        HttpResponseMessage httpResponse = null!;
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        try
        {
            httpResponse = await _httpClient.PostAsync(_options.EndpointToVerifyCustomer, httpContent);
        }
        catch (Exception ex)
        {
            Result.Failure(InfrastructureError.HttpClientFail);
        }

        if (httpResponse.ShouldRetry(_options.StatusCodesToRetry))
            Result.Failure(new Error("",$"PostAsync({_options.EndpointToVerifyCustomer})"));

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<bool>(content);

        return Result.Success(response);
    }

    public async Task<Order> CreateTicket(VerifyCustomerDto request)
    {
        HttpResponseMessage httpResponse;
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        try
        {
            httpResponse = await _httpClient.PostAsync(_options.EndpointToVerifyCustomer, httpContent);
        }
        catch (Exception ex)
        {

            throw new RetryableException(ex.Message);
        }

        if (httpResponse.ShouldRetry(_options.StatusCodesToRetry))
            throw new RetryableException($"PostAsync({_options.EndpointToVerifyCustomer})");

        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Order>(content);

        return response;
    }
}
