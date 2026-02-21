using Edri.Domain.Constants;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder
            .Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Tenant.Name);

        builder.HasData(new Tenant(
            Ids.Seed.TenantId,
            "Admin Tenant"));
    }
}