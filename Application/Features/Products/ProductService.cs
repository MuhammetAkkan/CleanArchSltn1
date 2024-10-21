using Application.Contracts.Persistance;
using Application.Features.Products.Create;
using Application.Features.Products.Update;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Application.Features.Products;

public class ProductService(IProductRepository productRepository, IMapper mapper, IValidator<CreateProductRequest> createValidator, IValidator<UpdateProductRequest> updateValidator, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAllAsync();

        var result = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(result);
    }


    public async Task<ServiceResult<List<ProductDto>>> GetPagedListAsnyc(int pageNumber, int pageSize)
    {
        var skipValue = (pageNumber - 1) * pageSize;

        var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);

        #region manuelMapping
        //var asProductsDto = products.Select(x => new ProductDTO(x.Id, x.Name, x.Price, x.Stock)).ToList();
        #endregion


        var productAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }   


    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);

        var result = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(result);
    }


    public async Task<ServiceResult<List<ProductDto>>> GetMinStockProducts(int minStockCount)
    {
        var products = await productRepository.GetMinStockProducts(minStockCount);

        var result = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(result);
    }


    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found");
        }

        var result = mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto?>.Success(result);
    }


    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var anyProduct = await productRepository.AnyAsync(i => i.Name == request.Name);

        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Product already exists");
        }

        var product = mapper.Map<Product>(request);

        await productRepository.CreateAsync(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));

    }


    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult.Fail("Product not found");
        }

        mapper.Map(request, product);

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }


    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult.Fail("Product not found");
        }

        productRepository.Delete(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }


    public async Task<ServiceResult> EnterDataInStock(int id, int stockCount)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult.Fail("Product not found");
        }

        product.Stock += stockCount;

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }


    public async Task<ServiceResult> UpdatePrice(int id, decimal price)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult.Fail("Product not found");
        }

        product.Price = price;

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }


    public async Task<ServiceResult<ProductDto>> GetPriceWithKdv(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ServiceResult<ProductDto>.Fail("Product not found");
        }

        var productDto = mapper.Map<ProductDto>(product);

        productDto = productDto with { Price = productDto.Price + productDto.Price * 0.18m };

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult<ProductDto>.Success(productDto);
    }
}