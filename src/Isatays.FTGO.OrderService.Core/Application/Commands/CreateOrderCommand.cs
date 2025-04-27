using Isatays.FTGO.OrderService.Core.DTO;
using KDS.Primitives.FluentResult;
using Mediator;

namespace Isatays.FTGO.OrderService.Core.Application.Commands;

public class CreateOrderCommand : ICommand<Result>
{
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public string DeliveryAddress { get; set; }
    public string PaymentMethod { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}