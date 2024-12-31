using Microsoft.EntityFrameworkCore;
using User_Api.Infrastructure.Entities;

namespace User_Api.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<InternalUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InternalUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(150);

        builder.Property(x => x.LastName)
            .HasMaxLength(250);

        builder.Property(x => x.Email)
            .HasMaxLength(250);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(x => x.HashedPassword)
            .IsRequired();

        builder.Property(x => x.Salt)
            .IsRequired();
    }
}
