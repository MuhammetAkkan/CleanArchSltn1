using Clean.Api.ExceptionHandler;
using Microsoft.AspNetCore.Diagnostics;

namespace Clean.Api.Extensions;

public static class ExceptionHandlerExtensions
{

    public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
    {
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

}