using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using User_Api.Features.CreateUser;
using User_Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("User");
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlite(connectionString);
});

builder.AddSharedServices((ctx, cfg) =>
{
    cfg.ReceiveEndpoint("create-user", e =>
    {
        e.Consumer(typeof(CreateUserConsumer), (_) => new CreateUserConsumer(ctx.GetRequiredService<IMediator>()));
    });
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
