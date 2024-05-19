namespace Isatays.FTGO.OrderService.Core.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}