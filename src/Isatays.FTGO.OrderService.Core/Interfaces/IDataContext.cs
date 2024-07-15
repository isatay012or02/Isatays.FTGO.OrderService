using Isatays.FTGO.OrderService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Isatays.FTGO.OrderService.Core.Interfaces; 

public interface IDataContext 
{
    DbSet<Order> Orders { get; set; } 
    
    DatabaseFacade Database { get; }
    
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
