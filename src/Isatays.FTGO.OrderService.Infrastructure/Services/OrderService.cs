using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Isatays.FTGO.OrderService.Infrastructure.Clients;
using KDS.Primitives.FluentResult;
using MassTransit;

namespace Isatays.FTGO.OrderService.Infrastructure.Services; 

public class OrderService : IOrderService
{
	private readonly CustomerServiceHttpClient _httpClient;
    private readonly IPublishEndpoint _publishEndpoint;

	public OrderService(IPublishEndpoint publishEndpoint, CustomerServiceHttpClient httpClient)
	{
		_publishEndpoint = publishEndpoint;
		_httpClient = httpClient;
	}

	public async Task<Result> CreateOrder(Guid id, string name, string email)
	{
		await _httpClient.VerifyCustomer(new(id, name, email));

		await _publishEndpoint.Publish(new OrderCreated
		{
			Id = id,
			Name = name,
			Email = email
		});

		return Result.Success();
	} 
}
