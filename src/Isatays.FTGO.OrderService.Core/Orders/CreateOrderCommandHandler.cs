using Isatays.FTGO.OrderService.Core.DTO;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Isatays.FTGO.OrderService.Core.Payments;
using MassTransit;

namespace Isatays.FTGO.OrderService.Core.Orders;

public class CreateOrderCommandHandler(IOrderService service) : IConsumer<CreateOrderCommand>
{
    public async Task Consume(ConsumeContext<CreateOrderCommand> context)
    {
        var command = context.Message;

        var order = new Order
        {
            OrderId = command.OrderId,
            CustomerId = command.CustomerId,
            
        };

        await service.CreateOrder(order);
        
        await context.Publish(new ProcessPaymentCommand(command.OrderId, 100));
    }
}