using Application;
using Application.Contracts.Caching;
using Caching;
using Clean.Api.ExceptionHandler;
using Clean.Api.Extensions;
using Clean.Api.Filters;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithFiltersExt();
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGenExt();

builder.Services.AddRepositories(builder.Configuration).AddServices (builder.Configuration);


//exception Handler ı ekledik.
builder.Services.AddExceptionHandlerExt();

//added Cache
builder.Services.AddCashingExt();

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
