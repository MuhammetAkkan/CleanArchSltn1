using Application.Features.Products.Create;
using FluentValidation;

namespace Application.Features.User.Create;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(i => i.name)
            .NotEmpty().WithMessage($"Kullanıcı ismi boş olamaz!")
            .NotNull().WithMessage($"Kullanıcı ismi null olamaz!")
            .MinimumLength(2).WithMessage($"Kullanıcı ismi en az 2 karakter olmalıdır!")
            .MaximumLength(100).WithMessage($"Kullanıcı ismi en fazla 100 karakter olmalıdır!");
    }
}