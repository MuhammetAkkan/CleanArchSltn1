using Domain.Entities;

namespace Application.Contracts.Persistance;

public interface ICategoryRepository : IGenericRepository<Category, int>
{
    Task<Category?> GetCategoryWithProductsAsync(int id);

    Task<List<Category>> GetCategoryWithProductsAsync();
}