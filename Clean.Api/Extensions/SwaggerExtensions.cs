namespace Clean.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Clean.Api", Version = "v1" });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean.Api v1"));

        return app;
    }
}