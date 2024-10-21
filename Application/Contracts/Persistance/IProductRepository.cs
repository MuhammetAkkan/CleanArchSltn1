using Domain.Entities;

namespace Application.Contracts.Persistance;

public interface IProductRepository : IGenericRepository<Product, int>
{

    public Task<List<Product>> GetTopPriceProductsAsync(int count);

    public Task<List<Product>> GetMinStockProducts(int minStockCount);

}