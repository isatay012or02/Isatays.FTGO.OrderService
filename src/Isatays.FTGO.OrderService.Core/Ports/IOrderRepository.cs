using Isatays.FTGO.OrderService.Core.Entities;
using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Core.Ports;

public interface IOrderRepository
{
    Task<Result> AddAsync(Order order);
}