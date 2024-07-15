using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Isatays.FTGO.OrderService.Api.Models;

public record PaymentDetailsRequest(
    [Required] [JsonProperty("paymentMethod")] string PaymentMethod,
    [JsonProperty("isPaymentCompleted")] bool IsPaymentCompleted 
    );