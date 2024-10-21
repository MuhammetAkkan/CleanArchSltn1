using Application.Features.Categories;
using Application.Features.Categories.Create;
using Application.Features.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => CustomActionResult(await categoryService.GetCategoryWithProductsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => CustomActionResult(await categoryService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        => CustomActionResult(await categoryService.CreateCategoryAsync(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request)
        => CustomActionResult(await categoryService.UpdateCategoryAsync(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => CustomActionResult(await categoryService.DeleteCategoryAsync(id));

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts()
        => CustomActionResult(await categoryService.GetCategoryWithProductsAsync());

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id)
        => CustomActionResult(await categoryService.GetCategoryWithProductsAsync(id));




}