using Application.Contracts.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product, int>(context), IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Product>> GetMinStockProducts(int minStockCount) =>
        await _context.Products.Where(i => i.Stock < minStockCount).ToListAsync();


    public async Task<List<Product>> GetTopPriceProductsAsync(int count) => await _context.Products.OrderByDescending(i => i.Price).Take(count).ToListAsync();
}