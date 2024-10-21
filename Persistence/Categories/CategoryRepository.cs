using Application.Contracts.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{

    private readonly AppDbContext _context = context;
    public async Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        var category = await _context.Categories.Include(i => i.Products).FirstOrDefaultAsync(i => i.Id == id);

        return category;
    }

    public async Task<List<Category>> GetCategoryWithProductsAsync()
    {
        var categories = await _context.Categories.Include(i => i.Products).ToListAsync();

        return categories;
    }
}