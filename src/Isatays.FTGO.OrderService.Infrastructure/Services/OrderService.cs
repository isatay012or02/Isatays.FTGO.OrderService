using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Isatays.FTGO.OrderService.Infrastructure.Clients;
using Isatays.FTGO.OrderService.Infrastructure.Persistence;
using KDS.Primitives.FluentResult;
using MassTransit;

namespace Isatays.FTGO.OrderService.Infrastructure.Services; 

public class OrderService : IOrderService
{
	private readonly CustomerServiceHttpClient _httpClient;
    private readonly IPublishEndpoint _publishEndpoint;
	private readonly DataContext _dataContext;

	public OrderService(IPublishEndpoint publishEndpoint, 
		CustomerServiceHttpClient httpClient,
        DataContext dataContext)
	{
		_publishEndpoint = publishEndpoint;
		_httpClient = httpClient;
		_dataContext = dataContext;
	}

	public async Task<Result> CreateOrder(Guid id, string name, string email)
	{
		await _httpClient.VerifyCustomer(new(id, name, email));

		using (var trans = _dataContext.Database.BeginTransaction())
		{
			try
			{
                var order = new Order
                {
                    Id = id,
                    Name = name,
                    Email = email
                };

                await _dataContext.AddAsync(order);
                await _dataContext.SaveChangesAsync();

                await _publishEndpoint.Publish(new OrderCreated
                {
                    Id = id,
                    Name = name,
                    Email = email
                });

                trans.Commit();
            }
			catch (Exception ex)
			{
				trans.Rollback();
			}
			
		}

		return Result.Success();
	} 
}
