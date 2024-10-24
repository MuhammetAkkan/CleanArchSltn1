using Clean.Api.Filters;

namespace Clean.Api.Extensions;

public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
    {
        services.AddScoped(typeof(NotFoundFilter<,>));

        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //null kontrolü yapma
        });

        return services;
    }
}