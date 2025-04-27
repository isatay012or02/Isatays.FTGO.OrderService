using Isatays.FTGO.OrderService.Core.Entities.Enums;

namespace Isatays.FTGO.OrderService.Core.Entities;

public class Order
{
    public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string DeliveryAddress { get; private set; }
        public string PaymentMethod { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? EstimatedDeliveryTime { get; private set; }
        public List<OrderItem> Items { get; private set; } = [];
    
    // Constructor for new order
    private Order(Guid customerId, Guid restaurantId, string deliveryAddress, string paymentMethod, List<OrderItem> items)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        Status = OrderStatus.Created;
        DeliveryAddress = deliveryAddress;
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.UtcNow;
        TotalPrice = CalculateTotalPrice(items);
        Items = items;
    }

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
    
    private decimal CalculateTotalPrice(List<OrderItem> items)
    {
        return items.Sum(item => item.Price * item.Quantity);
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
    }
    
    public void MarkAsDelivered()
    {
        if (Status == OrderStatus.InDelivery)
        {
            Status = OrderStatus.Delivered;
        }
    }
    
    public void CancelOrder()
    {
        if (Status == OrderStatus.Created || Status == OrderStatus.InDelivery)
        {
            Status = OrderStatus.Cancelled;
        }
    }
    
    public static Order Create(Guid customerId, Guid restaurantId, string deliveryAddress, string paymentMethod, List<OrderItem> items)
    {
        // Validate inputs
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID cannot be empty", nameof(customerId));

        if (restaurantId == Guid.Empty)
            throw new ArgumentException("Restaurant ID cannot be empty", nameof(restaurantId));

        if (string.IsNullOrEmpty(deliveryAddress))
            throw new ArgumentException("Delivery address cannot be empty", nameof(deliveryAddress));

        if (items == null || !items.Any())
            throw new ArgumentException("Order must contain at least one item", nameof(items));

        return new Order(customerId, restaurantId, deliveryAddress, paymentMethod, items);
    }
}
