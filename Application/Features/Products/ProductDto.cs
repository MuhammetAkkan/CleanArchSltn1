﻿namespace Application.Features.Products;

public record ProductDto(int Id, string Name, decimal Price, int Stock, int CategoryId);