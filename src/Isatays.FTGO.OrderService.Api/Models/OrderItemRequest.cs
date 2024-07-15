using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record OrderItemRequest(
    [Required] [JsonProperty("menuItemId")] int MenuItemId,
    [Required] [JsonProperty("quantity")] 
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")] int Quantity
    );