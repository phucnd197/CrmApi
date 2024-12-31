using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIM_Api.Infrastructure.Entities;

namespace PIM_Api.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasMaxLength(100);
        builder.Property(x => x.Name)
            .HasMaxLength(250);
        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Prices)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
