using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Repositories;

namespace Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedResult<ProductListDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<PagedResult<ProductListDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var allProducts = await _productRepository.GetAllAsync(cancellationToken);

        var productDtos = allProducts
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Status = p.Status.ToString()
            })
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var pagedResult = new PagedResult<ProductListDto>(
            productDtos,
            allProducts.Count,
            request.PageNumber,
            request.PageSize
        );

        return Result<PagedResult<ProductListDto>>.Success(pagedResult);
    }
}
