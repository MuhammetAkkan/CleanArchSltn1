using Application.Features.Categories.Create;
using Application.Features.Categories.Update;

namespace Application.Features.Categories;

public interface ICategoryService
{
    Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id);

    Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();

    Task<ServiceResult<int>> CreateCategoryAsync(CreateCategoryRequest request);

    Task<ServiceResult> UpdateCategoryAsync(int id, UpdateCategoryRequest request);

    Task<ServiceResult> DeleteCategoryAsync(int id);

    Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();

    Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
}