namespace Isatays.FTGO.OrderService.Core.Entities;

public class Customer
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;
}
