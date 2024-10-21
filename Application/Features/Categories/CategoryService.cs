using System.Net;
using Application.Contracts.Persistance;
using Application.Features.Categories.Create;
using Application.Features.Categories.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork) : ICategoryService
{

    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(id);

        if (category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail($"category is null");
        }

        var result = mapper.Map<CategoryWithProductsDto>(category);

        return ServiceResult<CategoryWithProductsDto>.Success(result);
    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        var categories = await categoryRepository.GetCategoryWithProductsAsync();

        var result = mapper.Map<List<CategoryWithProductsDto>>(categories);

        return ServiceResult<List<CategoryWithProductsDto>>.Success(result);
    }

    public async Task<ServiceResult<int>> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var category = mapper.Map<Category>(request);

        await categoryRepository.CreateAsync(category);

        await unitOfWork.SaveChangesAsync();

        string url = $"api/categories/{category.Id}";

        return ServiceResult<int>.SuccessAsCreated(category.Id, url);
    }

    public async Task<ServiceResult> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult.Fail("Category not found");
        }

        mapper.Map(request, category);

        categoryRepository.Update(category);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(statusCode: HttpStatusCode.OK);
    }

    public async Task<ServiceResult> DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult.Fail("Category not found");
        }

        categoryRepository.Delete(category);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAllAsync();

        var result = mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.Success(result);
    }

    public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult<CategoryDto?>.Fail($"Category is not fount:");
        }

        var result = mapper.Map<CategoryDto>(category);

        return ServiceResult<CategoryDto?>.Success(result);

    }
}