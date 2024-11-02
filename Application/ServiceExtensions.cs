using Application.Features.Categories;
using Application.Features.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Contracts.Persistance;
using Application.Features.Roles;
using Application.Features.User;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IRoleService, RoleService>();



        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        //fluent validation ekledik.
        services.AddFluentValidationAutoValidation();

        //
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly()); //AutoMapper ekledik.

        

        return services;
        // Add your repository extensions here

    }
}