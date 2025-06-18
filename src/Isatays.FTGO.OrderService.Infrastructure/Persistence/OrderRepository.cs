using Isatays.FTGO.OrderService.Core.Common.Constants;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Ports;
using KDS.Primitives.FluentResult;
using Microsoft.Extensions.Logging;

namespace Isatays.FTGO.OrderService.Infrastructure.Persistence;

public class OrderRepository(
    IDataContext dataContext, 
    ILogger<OrderRepository> logger
    )
    : IOrderRepository
{
    public async Task<Result> AddAsync(Order order)
    {
        try
        {
            await dataContext.Orders.AddAsync(order);
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", $"Exception message: {ex.Message}");
            return Result.Failure(DomainError.DatabaseFailed);
        }

        return Result.Success();
    }
}