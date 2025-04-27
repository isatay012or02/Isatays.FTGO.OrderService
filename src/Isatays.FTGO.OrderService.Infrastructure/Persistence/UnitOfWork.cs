using Isatays.FTGO.OrderService.Core.Ports;

namespace Isatays.FTGO.OrderService.Infrastructure.Persistence;

public class UnitOfWork(
    IOrderRepository orderRepository, 
    DataContext context)
: IUnitOfWork
{
    public IOrderRepository OrderRepository { get; set; } = orderRepository;
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}