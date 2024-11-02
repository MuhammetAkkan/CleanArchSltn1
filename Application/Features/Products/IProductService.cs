using Application.Features.Products.Create;
using Application.Features.Products.Update;

namespace Application.Features.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();

    Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);

    Task<ServiceResult<List<ProductDto>>> GetMinStockProducts(int minStockCount);

    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);

    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);


    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);

    Task<ServiceResult> DeleteAsync(int id);


    Task<ServiceResult> EnterDataInStock(int id, int stockCount);

    Task<ServiceResult> UpdatePrice(int id, decimal price);

    Task<ServiceResult<ProductDto>> GetPriceWithKdv(int id);
}