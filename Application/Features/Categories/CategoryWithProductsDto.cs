using Domain.Entities;

namespace Application.Features.Categories;

public record CategoryWithProductsDto(int id, string Name, List<Product>? Products);
