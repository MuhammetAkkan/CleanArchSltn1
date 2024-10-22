using Application;
using Application.Contracts.Caching;
using Caching;
using Clean.Api.ExceptionHandler;
using Clean.Api.Filters;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //null kontrolü yapma

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration).AddServices (builder.Configuration);

//FluentValidation ekledik.
builder.Services.AddScoped(typeof(NotFoundFilter<,>));

//exception Handler ı ekledik.
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

//added Cache
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>(); //sürekli aynı cache service kullanılacak, aynı bellekten yararlanılacak.

var app = builder.Build();

// biz yazdık.
app.UseExceptionHandler(i => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
