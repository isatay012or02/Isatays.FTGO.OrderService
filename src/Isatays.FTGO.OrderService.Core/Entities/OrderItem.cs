namespace Isatays.FTGO.OrderService.Core.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string SpecialInstructions { get; private set; }

    public OrderItem(string name, decimal price, int quantity, string specialInstructions = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Quantity = quantity;
        SpecialInstructions = specialInstructions;
    }
}