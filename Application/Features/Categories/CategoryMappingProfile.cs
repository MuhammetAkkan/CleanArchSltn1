using Application.Features.Categories.Create;
using Application.Features.Categories.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Category, CategoryWithProductsDto>();

        CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name!.ToLowerInvariant()));

        //inputtan gelen name i toLower luyoruz, ve bu bir reverse değil tek taraflı bir mapping
        CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name!.ToLowerInvariant()));
    }
}