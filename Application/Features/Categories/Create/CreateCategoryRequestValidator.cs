using FluentValidation;

namespace Application.Features.Categories.Create;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(i=>i.Name)
            .NotEmpty().WithMessage("Kategori ismi boş olamaz!")
            .NotNull().WithMessage("Kategori ismi null olamaz!")
            .MinimumLength(2).WithMessage("Kategori ismi en az 2 karakter olmalıdır!")
            .MaximumLength(100).WithMessage("Kategori ismi en fazla 100 karakter olmalıdır!");
    }
}