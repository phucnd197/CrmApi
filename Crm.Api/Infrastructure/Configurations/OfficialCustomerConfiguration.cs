using Crm_Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm_Api.Infrastructure.Configurations;

public class OfficialCustomerConfiguration : IEntityTypeConfiguration<OfficialCustomer>
{
    public void Configure(EntityTypeBuilder<OfficialCustomer> builder)
    {
        builder.HasKey(x => x.CustomerId);
    }
}
