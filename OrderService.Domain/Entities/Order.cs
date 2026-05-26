namespace OrderService.Domain;

using OrderService.Domain.Enums;

public class Order
{
    private readonly List<OrderItem> _items = [];

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal TotalAmount => _items.Sum(x => x.Total);

    private Order() { }

    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();

        CustomerId = customerId;

        Status = OrderStatus.Pending;

        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        _items.Add(new OrderItem(
            productId,
            productName,
            unitPrice,
            quantity));
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException(
                "Only pending orders can be paid");

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Shipped)
            throw new InvalidOperationException(
                "Shipped orders cannot be cancelled");

        Status = OrderStatus.Cancelled;
    }
}