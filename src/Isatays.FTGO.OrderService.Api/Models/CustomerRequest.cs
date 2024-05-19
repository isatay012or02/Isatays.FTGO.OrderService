using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record CustomerRequest(
        [JsonProperty("id")]int CustomerId,
        [JsonProperty("name")]string Name,
        [JsonProperty("email")]string Email,
        [JsonProperty("phoneNumber")]string PhoneNumber
    );