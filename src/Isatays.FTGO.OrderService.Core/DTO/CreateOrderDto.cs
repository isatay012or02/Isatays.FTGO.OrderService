namespace Isatays.FTGO.OrderService.Core.DTO;

public record CreateOrderDto
{
    public string OrderName { get; set; } = string.Empty;

    public string OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string TicketName { get; set; } = string.Empty;

    public string TicketDescription { get; set; } = string.Empty;

    public string CardNumber { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public int CardCode { get; set; }
}
