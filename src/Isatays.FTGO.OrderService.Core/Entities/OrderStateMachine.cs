using Isatays.FTGO.OrderService.Core.Orders;
using Isatays.FTGO.OrderService.Core.Payments;
using MassTransit;

namespace Isatays.FTGO.OrderService.Core.Entities;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State OrderCreated { get; private set; }
    public State InventoryReserved { get; private set; }
    public State PaymentProcessed { get; private set; }

    public Event<CreateOrderCommand> CreateOrderEvent { get; private set; }
    
    public Event<ProcessPaymentCommand> ProcessPaymentEvent { get; private set; }

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateOrderEvent, x => x.CorrelateById(context => context.Message.OrderId));

        Initially(
            When(CreateOrderEvent)
                .Then(context => {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.CustomerId = context.Data.CustomerId;
                    context.Instance.Items = context.Data.Items;
                })
                .TransitionTo(OrderCreated)
                .Publish(context => new ProcessPaymentCommand(context.Instance.OrderId, 100))
        );

        During(InventoryReserved,
            When(ProcessPaymentEvent)
                .TransitionTo(PaymentProcessed)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}