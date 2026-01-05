using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Repositories;

namespace Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return Result<ProductDto>.Failure($"Product with ID {request.Id} not found");
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Status = product.Status.ToString(),
            CreatedAt = product.CreatedAt
        };

        return Result<ProductDto>.Success(productDto);
    }
}
