using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record CreateOrderRequest([JsonProperty("id")] Guid Id, [JsonProperty("name")] string Name, [JsonProperty("email")] string Email);
