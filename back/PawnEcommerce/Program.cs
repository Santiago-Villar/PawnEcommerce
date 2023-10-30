using PawnEcommerce.Middlewares;
﻿using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Product;
using Service.Sale;
using Service.Session;
using Service.User;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IColorService, ColorService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ISaleService, SaleService>();
        builder.Services.AddScoped<ISessionService, SessionService>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<IColorRepository, ColorRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddHttpContextAccessor();


        builder.Services.AddControllers();
        builder.Services.AddDbContext<EcommerceContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext"),
                b => b.MigrationsAssembly("PawnEcommerce")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });

        var app = builder.Build();

        app.UseCors("AllowAllOrigins");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<AuthorizationMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
