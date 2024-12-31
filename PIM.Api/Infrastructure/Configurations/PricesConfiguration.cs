using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIM_Api.Infrastructure.Entities;

namespace PIM_Api.Infrastructure.Configurations;

public class PricesConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Currency)
            .HasMaxLength(20);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Prices)
            .HasForeignKey(x => x.ProductId);
    }
}
