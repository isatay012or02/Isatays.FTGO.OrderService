using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isatays.FTGO.OrderService.Core.Entities;

[Table("Order")]
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Column("customerId")]
    public string Name { get; set; }

    [Column("email")]
    public string Email { get; set; }
}
