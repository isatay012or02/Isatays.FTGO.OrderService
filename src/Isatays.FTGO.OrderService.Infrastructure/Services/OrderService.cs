using Isatays.FTGO.OrderService.Core.Common.Constants;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Interfaces;
using Isatays.FTGO.OrderService.Infrastructure.Clients;
using Isatays.FTGO.OrderService.Infrastructure.Common.Constants;
using KDS.Primitives.FluentResult;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Isatays.FTGO.OrderService.Infrastructure.Services; 

public class OrderService(
	IPublishEndpoint publishEndpoint,
	CustomerServiceHttpClient httpClient,
	IDataContext dataContext,
	ILogger<OrderService> logger)
	: IOrderService
{
	public async Task<Result> CreateOrder(Order order)
	{
		var resultOfVerify = await httpClient.VerifyCustomer(order.Customer);
		if (resultOfVerify.Value)
			return Result.Failure(InfrastructureError.ValidationFail);

		await using (var trans = await dataContext.Database.BeginTransactionAsync())
		{
			try
			{
				await dataContext.Orders.AddAsync(order);
				
				await Task.WhenAll(
					dataContext.SaveChangesAsync(),
					publishEndpoint.Publish(order));
				
                await trans.CommitAsync();
            }
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				logger.LogError("{Message}", $"Exception message: {ex.Message}");
				return Result.Failure(DomainError.DatabaseFailed);
			}
		}

		return Result.Success();
	} 
}
