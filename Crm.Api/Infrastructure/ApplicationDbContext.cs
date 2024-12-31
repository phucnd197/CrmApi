using System.Reflection;
using Crm_Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm_Api.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<OfficialCustomer> OfficialCustomers { get; set; }

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
