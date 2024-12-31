using System.Reflection;
using Crm_Api.Contracts.Request;
using Crm_Api.Features.RegisterCustomer;
using Crm_Api.Features.RegisterPricingAgreement;
using Crm_Api.Infrastructure;
using Crm_Api.Shared.Clients;
using FastEndpoints;
using FluentValidation;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.EntityFrameworkCore;
using Shared;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(c => { c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
builder.Services.AddFastEndpoints();
builder.AddSharedServices((ctx, cfg) => { });

var connectionString = builder.Configuration.GetConnectionString("CrmApi");
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlite(connectionString);
});
var connection = await ConnectionMultiplexer.ConnectAsync(builder.Configuration.GetConnectionString("redis"));
var db = connection.GetDatabase();
builder.Services.AddSingleton<IDistributedLockProvider>(_ => new RedisDistributedSynchronizationProvider(db));

#region Add Validators
builder.Services.AddScoped<IValidator<RegisterCustomerRequest>, RegisterCustomerRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterPricingAgreementRequest>, RegisterPricingAgreementRequestValidator>();
#endregion

#region Add thirdparty client
builder.Services.AddUserClient();
builder.Services.AddPIMClient();
builder.Services.AddCRMClient();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapFastEndpoints();

app.Run();