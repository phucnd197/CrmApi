using System.Reflection;
using Microsoft.EntityFrameworkCore;
using User_Api.Infrastructure.Entities;

namespace User_Api.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<InternalUser> InternalUsers { get; set; }

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
