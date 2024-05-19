using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record CreateOrderRequest(
    [JsonProperty("name")] string Name, 
    [JsonProperty("email")] string Email,
    [JsonProperty("customer")] CustomerRequest Customer
    );
