using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Entities.Enums;

namespace Isatays.FTGO.OrderService.Core.DTO;

public record CreateOrderDto(int OrderId, int CustomerId, Customer Customer, DateTime OrderDate, 
    DateTime? DeliveryDate, List<OrderItem> Items, OrderStatus Status, decimal TotalAmount);
