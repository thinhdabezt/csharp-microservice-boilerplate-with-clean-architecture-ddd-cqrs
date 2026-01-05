using Domain.Common;

namespace Domain.Entities;

// Sample Aggregate Root - Product
public class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public ProductStatus Status { get; private set; }

    // Navigation properties
    private readonly List<ProductReview> _reviews = new();
    public IReadOnlyCollection<ProductReview> Reviews => _reviews.AsReadOnly();

    private Product() { } // EF Core

    public Product(string name, string description, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));

        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative", nameof(stock));

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Status = ProductStatus.Active;

        AddDomainEvent(new ProductCreatedEvent(Id, Name));
    }

    public void UpdateDetails(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));

        Name = name;
        Description = description;
        Price = price;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductUpdatedEvent(Id, Name));
    }

    public void UpdateStock(int quantity)
    {
        if (Stock + quantity < 0)
            throw new InvalidOperationException("Insufficient stock");

        Stock += quantity;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductStockChangedEvent(Id, Stock));
    }

    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = ProductStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddReview(ProductReview review)
    {
        _reviews.Add(review);
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum ProductStatus
{
    Active = 1,
    Inactive = 2,
    Discontinued = 3
}

// Sample Entity (not an aggregate root)
public class ProductReview : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string ReviewerName { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }

    private ProductReview() { } // EF Core

    public ProductReview(Guid productId, string reviewerName, int rating, string comment)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        ProductId = productId;
        ReviewerName = reviewerName;
        Rating = rating;
        Comment = comment;
    }
}
