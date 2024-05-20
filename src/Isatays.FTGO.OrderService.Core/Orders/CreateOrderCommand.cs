using Isatays.FTGO.OrderService.Core.Entities;
using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.OrderService.Core.Orders;

public class CreateOrderCommand : IRequest<Result>
{
    public CreateOrderCommand(int customerId, PaymentDetails payment, DeliveryAddress deliveryAddress, 
        string notes, List<OrderItem> items)
    {
        CustomerId = customerId;
        Payment = payment;
        DeliveryAddress = deliveryAddress;
        Notes = notes;
        Items = items;
    }

    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }
    public PaymentDetails Payment { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public string Notes { get; set; }
}
