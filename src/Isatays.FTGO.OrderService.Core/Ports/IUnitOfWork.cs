namespace Isatays.FTGO.OrderService.Core.Ports;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}