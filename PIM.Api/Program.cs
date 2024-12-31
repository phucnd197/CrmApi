using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PIM_Api.Infrastructure;
using PIM_Api.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var connectionString = builder.Configuration.GetConnectionString("PIM");
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlite(connectionString);
    if (builder.Environment.IsDevelopment())
    {
        config.UseSeeding((context, _) =>
        {
            context.Set<Product>().Add(new Product { Id = 1, Code = "Test1", Name = "Test product 1", Description = "Test product 1 for development" });
            context.Set<Price>().AddRange(new[]
            {
                new Price
                {
                    ProductId = 1,
                    BasePrice = 100,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
                },
                new Price
                {
                    ProductId = 1,
                    BasePrice = 120,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-200)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100))
                },
            });

            context.Set<Product>().Add(new Product { Id = 2, Code = "Test2", Name = "Test product 2", Description = "Test product 2 for development" });
            context.Set<Price>().AddRange(new[]
            {
                new Price
                {
                    ProductId = 2,
                    BasePrice = 100,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
                },
                new Price
                {
                    ProductId = 2,
                    BasePrice = 120,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-200)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100))
                },
            });
            context.SaveChanges();
        });
        config.UseAsyncSeeding(async (context, _, ct) =>
        {
            context.Set<Product>().Add(new Product { Id = 1, Code = "Test1", Name = "Test product 1", Description = "Test product 1 for development" });
            context.Set<Price>().AddRange(new[]
            {
                new Price
                {
                    ProductId = 1,
                    BasePrice = 100,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
                },
                new Price
                {
                    ProductId = 1,
                    BasePrice = 120,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-200)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100))
                },
            });

            context.Set<Product>().Add(new Product { Id = 2, Code = "Test2", Name = "Test product 2", Description = "Test product 2 for development" });
            context.Set<Price>().AddRange(new[]
            {
                new Price
                {
                    ProductId = 2,
                    BasePrice = 120,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
                },
                new Price
                {
                    ProductId = 2,
                    BasePrice = 100,
                    Currency="USD",
                    EffectiveDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-200)),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-100))
                },
            });
            await context.SaveChangesAsync(ct);
        });
    }
});



var app = builder.Build();

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
