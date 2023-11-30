using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isatays.FTGO.OrderService.Core.Entities;

[Table(name: "Order", Schema = "OrderService")]
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Column("customerId")]
    public string Name { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;
}
