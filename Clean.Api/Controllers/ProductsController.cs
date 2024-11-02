using Application.Features.Products;
using Application.Features.Products.Create;
using Application.Features.Products.Update;
using Clean.Api.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer"), RoleFilter("Admin")]
    public async Task<IActionResult> GetAll() 
        => CustomActionResult(await productService.GetAllListAsync());


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => CustomActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        => CustomActionResult(await productService.CreateAsync(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        => CustomActionResult(await productService.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => CustomActionResult(await productService.DeleteAsync(id));


}