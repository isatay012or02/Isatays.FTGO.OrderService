namespace Isatays.FTGO.OrderService.Core.DTO;

public class OrderItemDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }
}