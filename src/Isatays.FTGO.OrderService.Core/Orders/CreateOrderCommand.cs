using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Entities.Enums;
using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.OrderService.Core.Orders;

public class CreateOrderCommand : IRequest<Result>
{
    public CreateOrderCommand(int orderId, int customerId, Customer customer, DateTime orderDate, 
        DateTime deliveryDate, PaymentDetails payment, DeliveryAddress deliveryAddress, string notes, 
        List<OrderItem> items, OrderStatus status)
    {
        OrderId = orderId;
        CustomerId = customerId;
        Customer = customer;
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        Payment = payment;
        DeliveryAddress = deliveryAddress;
        Notes = notes;
        Items = items;
        Status = status;
    }

    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentDetails Payment { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public string Notes { get; set; }
}
