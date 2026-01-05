using Domain.Common;

namespace Domain.Entities;

public class ProductCreatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }

    public ProductCreatedEvent(Guid productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

public class ProductUpdatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }

    public ProductUpdatedEvent(Guid productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

public class ProductStockChangedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int NewStock { get; }

    public ProductStockChangedEvent(Guid productId, int newStock)
    {
        ProductId = productId;
        NewStock = newStock;
    }
}
