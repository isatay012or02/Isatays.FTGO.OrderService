using Isatays.FTGO.OrderService.Core.Application.Events;
using Isatays.FTGO.OrderService.Core.DTO;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Ports;
using KDS.Primitives.FluentResult;
using Mediator;

namespace Isatays.FTGO.OrderService.Core.Application.Commands;

public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IEventProducer eventProducer
        )
    : ICommandHandler<CreateOrderCommand, Result>
{
    public async ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderItems = command.Items.Select(i => 
            new OrderItem(i.Name, i.Price, i.Quantity, i.SpecialInstructions)).ToList();
            
        var order = Order.Create(
            command.CustomerId,
            command.RestaurantId,
            command.DeliveryAddress,
            command.PaymentMethod,
            orderItems
        );

        await orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            RestaurantId = order.RestaurantId,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            DeliveryAddress = order.DeliveryAddress,
            PaymentMethod = order.PaymentMethod,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(i => new OrderItemDto
            {
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                SpecialInstructions = i.SpecialInstructions
            }).ToList()
        };

        await eventProducer.ProduceAsync("create-order", orderCreatedEvent, cancellationToken);

        return Result.Success();
    }
}