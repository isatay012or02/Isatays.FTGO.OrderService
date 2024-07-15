using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record DeliveryAddressRequest(
    [Required] [JsonProperty("street")] string Street,
    [Required] [JsonProperty("city")] string City,
    [Required] [JsonProperty("postalCode")] string PostalCode,
    [Required] [JsonProperty("country")] string Country
    );