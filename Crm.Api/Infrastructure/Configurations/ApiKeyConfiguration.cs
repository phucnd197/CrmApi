using System.Globalization;
using Crm_Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm_Api.Infrastructure.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.HasData(new[]
        {
            new ApiKey
            {
                Id = Guid.Parse("4ffebbdf-fdd5-46ab-ae2b-db205056bb53"),
                Name = "Key for FakeThirdParty",
                Created = DateTime.ParseExact("31/12/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            }
        });
    }
}
