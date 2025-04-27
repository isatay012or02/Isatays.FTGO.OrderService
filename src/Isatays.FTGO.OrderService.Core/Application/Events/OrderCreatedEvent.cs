using Isatays.FTGO.OrderService.Core.DTO;
using Isatays.FTGO.OrderService.Core.Entities.Enums;

namespace Isatays.FTGO.OrderService.Core.Application.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public string DeliveryAddress { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}