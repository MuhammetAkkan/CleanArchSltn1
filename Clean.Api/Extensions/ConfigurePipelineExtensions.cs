namespace Clean.Api.Extensions;


//
public static class ConfigurePipelineExtensions
{
    public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
    {
        app.UseExceptionHandler(i => { });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerExt();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;

    }
}