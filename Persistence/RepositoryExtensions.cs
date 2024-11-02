using Application.Contracts.Persistance;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Categories;
using Persistence.Interceptors;
using Persistence.Products;
using Persistence.Roles;
using Persistence.Users;

namespace Persistence;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>(); //GetSection ile bu alana ulaşıyor, Get ile de nesneyi alıyoruz.

            options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName); //migration işlemleri için gerekli
            });

            #region AddedInterceptor
            options.AddInterceptors(new AuiditDbContextInterceptor());
            #endregion

        });

        //scoped lar dbContext ile ilgili olduğu için burada tanımlanmalıdır.

        //genel scoped
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        //Product scoped
        services.AddScoped<IProductRepository, ProductRepository>();

        //Category scoped
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();



        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
        // Add your repository extensions here

    }
}