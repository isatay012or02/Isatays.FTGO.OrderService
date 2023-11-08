using Isatays.FTGO.OrderService.Core.Interfaces;
using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.OrderService.Core.Orders;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IOrderService _orderService;

	public CreateOrderCommandHandler(IOrderService orderService)
	{
		_orderService = orderService;
	}

	public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var result = await _orderService.CreateOrder(request.Id, request.Name, request.Email);

		return result;
	}
}
