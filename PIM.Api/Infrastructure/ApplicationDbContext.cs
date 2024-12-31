using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PIM_Api.Infrastructure.Entities;

namespace PIM_Api.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Price> Prices { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
