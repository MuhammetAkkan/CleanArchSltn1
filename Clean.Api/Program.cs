using System.Text;
using Application;
using Application.Contracts.Caching;
using Application.Features.Roles;
using Application.Features.User;
using Caching;
using Clean.Api.ExceptionHandler;
using Clean.Api.Extensions;
using Clean.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle


builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

// JWT Authentication ayarları
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Muhammet Akkan",
        ValidAudience = "www.MuhammetAkkan.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key"))
    };
});

// Kontrolcüleri ve özel filtreleri ekle
builder.Services.AddControllersWithFiltersExt();

// Swagger/OpenAPI ayarları
builder.Services.AddSwaggerGen(setup =>
{
    var JwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Token ı buraya yaz",
        Type = SecuritySchemeType.Http,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    setup.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, JwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { JwtSecurityScheme, Array.Empty<string>() }
    });
});

// Diğer servisleri ekle
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

// Exception Handler ekle
builder.Services.AddExceptionHandlerExt();

// Cache ekle
builder.Services.AddCashingExt();

var app = builder.Build();

// Middleware pipeline yapılandır
app.UseConfigurePipelineExt();

// Swagger'ı kullan
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

// Auth middleware'ini kullan
app.UseAuthentication();
app.UseAuthorization();

// Kontrolcüleri harita
app.MapControllers();

// Uygulamayı çalıştır
app.Run();
