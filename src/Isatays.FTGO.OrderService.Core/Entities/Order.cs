using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Isatays.FTGO.OrderService.Core.Entities.Enums;

namespace Isatays.FTGO.OrderService.Core.Entities;

[Table("Order", Schema = "public")]
public class Order
{
    [Column("id")]
    public int OrderId { get; set; }
    
    [Column("customerId")]
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    [Column("date")]
    public DateTime OrderDate { get; set; }
    
    [Column("deliveryDate")]
    public DateTime? DeliveryDate { get; set; }
    
    public List<OrderItem> Items { get; set; } = new();
    
    [Column("status")]
    public OrderStatus Status { get; set; }
    public decimal TotalAmount => CalculateTotalAmount();
    public PaymentDetails Payment { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public string Notes { get; set; }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
        UpdateStatus();
    }
    
    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
        UpdateStatus();
    }
    
    private decimal CalculateTotalAmount()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.Price * item.Quantity;
        }
        return total;
    }
    
    public void UpdateStatus()
    {
        if (Items.Count == 0)
        {
            Status = OrderStatus.Empty;
        }
        else if (Status == OrderStatus.Empty && Items.Count > 0)
        {
            Status = OrderStatus.Created;
        }
        // Add more logic for updating status based on other conditions
    }
    
    public bool IsPaid()
    {
        return Payment != null && Payment.IsPaymentCompleted;
    }
    
    public void MarkAsDelivered()
    {
        if (Status == OrderStatus.InProgress)
        {
            Status = OrderStatus.Delivered;
            DeliveryDate = DateTime.Now;
        }
    }
    
    public void CancelOrder()
    {
        if (Status == OrderStatus.Created || Status == OrderStatus.InProgress)
        {
            Status = OrderStatus.Canceled;
        }
    }
}
