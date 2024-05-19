using Isatays.FTGO.OrderService.Core.Entities;
using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Core.Interfaces; 

public interface IOrderService
{
    Task<Result> CreateOrder(Order order);
}
