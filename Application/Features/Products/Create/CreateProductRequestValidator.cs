using FluentValidation;
using FluentValidation.Validators;

namespace Application.Features.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(i=> i.Name)
            .NotEmpty().WithMessage($"Ürün ismi boş olamaz!")
            .NotNull().WithMessage($"Ürün ismi null olamaz!")
            .MinimumLength(2).WithMessage($"Ürün ismi en az 2 karakter olmalıdır!")
            .MaximumLength(100).WithMessage($"Ürün ismi en fazla 100 karakter olmalıdır!");

        RuleFor(i => i.Price)
            .NotEmpty().WithMessage($"Ürün fiyatı boş olamaz!")
            .NotNull().WithMessage($"Ürün fiyatı null olamaz!")
            .GreaterThan(0).WithMessage($"Ürün fiyatı 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(100000).WithMessage($"Ürün fiyatı 100.000'den küçük olmalıdır!");

        RuleFor(i => i.Stock)
            .NotEmpty().WithMessage($"Ürün stok adedi boş olamaz!")
            .NotNull().WithMessage($"Ürün stok adedi null olamaz!")
            .GreaterThan(0).WithMessage($"Ürün stok adedi 0'dan büyük olmalıdır!")
            .LessThanOrEqualTo(100000).WithMessage($"Ürün stok adedi 100.000'den küçük olmalıdır!");

        RuleFor(i => i.CategoryId)
            .NotEmpty().WithMessage($"Kategori Id boş olamaz!")
            .NotNull().WithMessage($"Kategori Id null olamaz!")
            .GreaterThan(0).WithMessage($"Kategori Id 0'dan büyük olmalıdır!");



    }
}