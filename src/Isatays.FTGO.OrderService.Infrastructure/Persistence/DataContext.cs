using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace Isatays.FTGO.OrderService.Infrastructure.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IDataContext
{
    public DbSet<Order> Orders { get; set; } 
}
