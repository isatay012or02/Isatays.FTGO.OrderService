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
        await using (var trans = await dataContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dataContext.Orders.AddAsync(order);

                await dataContext.SaveChangesAsync();
				
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