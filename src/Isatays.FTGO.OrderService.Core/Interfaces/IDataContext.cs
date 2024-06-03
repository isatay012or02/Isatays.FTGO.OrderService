namespace Isatays.FTGO.OrderService.Core.Interfaces; 

public interface IDataContext 
{
    DatabaseFacade Database { get; }
    
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
