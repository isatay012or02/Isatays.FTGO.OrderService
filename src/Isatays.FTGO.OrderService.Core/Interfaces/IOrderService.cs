using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Core.Interfaces; 

public interface IOrderService 
{
    Task<Result> CreateOrder(Guid id, string name, string email);
}
