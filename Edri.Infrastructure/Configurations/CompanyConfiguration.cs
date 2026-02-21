using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Companies");
        builder.HasKey(c => c.Id);

        // 2. Propiedades
        builder.Property(c => c.TaxId)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder.Property(c => c.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.TradeName)
            .HasMaxLength(200);

        builder.Property(c => c.FiscalAddress)
            .HasMaxLength(300);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.LogoUrl)
            .HasMaxLength(500);

        // 3. Índices
        builder.HasIndex(c => c.TaxId).IsUnique();

        // ==============================================================================
        // SEED DATA: Companies
        // ==============================================================================
        builder.HasData(
            new
            {
                Id = Guid.Parse("e1000000-0000-0000-0000-000000000001"),
                TaxId = "20123456789",
                BusinessName = "Electro Oriente S.A.",
                TradeName = "Electro Oriente",
                FiscalAddress = "Av. Abelardo Quiñones 1245, Iquitos",
                Phone = "065123456",
                Email = "contacto@electrooriente.com.pe",
                LogoUrl = (string?)null
            },
            new
            {
                Id = Guid.Parse("e1000000-0000-0000-0000-000000000002"),
                TaxId = "20987654321",
                BusinessName = "Electrocentro S.A.",
                TradeName = "Electrocentro",
                FiscalAddress = "Jr. Amazonas 345, Huancayo",
                Phone = "064234567",
                Email = "contacto@electrocentro.com.pe",
                LogoUrl = (string?)null
            }
        );
    }
}