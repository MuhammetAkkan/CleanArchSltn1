using Application.Contracts.Caching;
using Caching;

namespace Clean.Api.Extensions;

public static class CashingExtensions
{

    public static IServiceCollection AddCashingExt(this IServiceCollection services)
    {
        services.AddMemoryCache();

        //sürekli newlememek için singleton olarak ekledik
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }


}