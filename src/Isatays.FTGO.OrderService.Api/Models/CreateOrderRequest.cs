using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record CreateOrderRequest(
    [Required] [JsonProperty("customerId")] int CustomerId,
    [Required] [JsonProperty("items")] List<OrderItemRequest> Items,
    [Required] [JsonProperty("deliveryAddress")] DeliveryAddressRequest DeliveryAddress, 
    [JsonProperty("notes")] string Notes,
    [JsonProperty("payment")] PaymentDetailsRequest Payment
    );
