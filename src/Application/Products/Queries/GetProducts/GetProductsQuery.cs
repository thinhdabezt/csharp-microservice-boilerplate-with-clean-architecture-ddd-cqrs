using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Products.Queries.GetProducts;

public record GetProductsQuery : IQuery<PagedResult<ProductListDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
