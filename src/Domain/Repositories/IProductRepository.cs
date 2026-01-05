using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default);
    Task<Product?> GetProductWithReviewsAsync(Guid id, CancellationToken cancellationToken = default);
}
