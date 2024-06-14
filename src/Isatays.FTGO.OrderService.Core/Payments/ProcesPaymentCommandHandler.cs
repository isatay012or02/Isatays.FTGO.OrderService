using MassTransit;

namespace Isatays.FTGO.OrderService.Core.Payments;

public class ProcessPaymentCommandHandler : IConsumer<ProcessPaymentCommand>
{
    public async Task Consume(ConsumeContext<ProcessPaymentCommand> context)
    {
        var command = context.Message;

    }
}