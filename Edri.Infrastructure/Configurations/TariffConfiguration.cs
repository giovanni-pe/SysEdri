using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Tariffs");
        builder.HasKey(t => t.Id);

        // 2. Propiedades
        builder.Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.PricePerKwh)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(t => t.FixedCharge)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.EffectiveFrom)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(t => new { t.CompanyId, t.Code }).IsUnique();

        // 4. Relación con Company
        builder.HasOne(t => t.Company)
            .WithMany()
            .HasForeignKey(t => t.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Tariffs
        // ==============================================================================
        var electroOrienteId = Guid.Parse("e1000000-0000-0000-0000-000000000001");
        var electrocentroId = Guid.Parse("e1000000-0000-0000-0000-000000000002");
        var effectiveDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Tarifas Electro Oriente
            new
            {
                Id = Guid.Parse("aa000000-0000-0000-0000-000000000001"),
                CompanyId = electroOrienteId,
                Code = "BT5B",
                Name = "Tarifa Residencial BT5B",
                PricePerKwh = 0.7520m,
                FixedCharge = 3.26m,
                Description = "Tarifa para consumo residencial hasta 100 kWh",
                EffectiveFrom = effectiveDate,
                EffectiveTo = (DateTime?)null,
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("aa000000-0000-0000-0000-000000000002"),
                CompanyId = electroOrienteId,
                Code = "BT5A",
                Name = "Tarifa Residencial BT5A",
                PricePerKwh = 0.8150m,
                FixedCharge = 3.26m,
                Description = "Tarifa para consumo residencial mayor a 100 kWh",
                EffectiveFrom = effectiveDate,
                EffectiveTo = (DateTime?)null,
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("aa000000-0000-0000-0000-000000000003"),
                CompanyId = electroOrienteId,
                Code = "BT4",
                Name = "Tarifa Comercial BT4",
                PricePerKwh = 0.6890m,
                FixedCharge = 5.50m,
                Description = "Tarifa para uso comercial",
                EffectiveFrom = effectiveDate,
                EffectiveTo = (DateTime?)null,
                IsActive = true
            },
            // Tarifas Electrocentro
            new
            {
                Id = Guid.Parse("aa000000-0000-0000-0000-000000000004"),
                CompanyId = electrocentroId,
                Code = "BT5B",
                Name = "Tarifa Residencial BT5B",
                PricePerKwh = 0.7350m,
                FixedCharge = 3.10m,
                Description = "Tarifa para consumo residencial hasta 100 kWh",
                EffectiveFrom = effectiveDate,
                EffectiveTo = (DateTime?)null,
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("aa000000-0000-0000-0000-000000000005"),
                CompanyId = electrocentroId,
                Code = "MT3",
                Name = "Tarifa Industrial MT3",
                PricePerKwh = 0.5200m,
                FixedCharge = 25.00m,
                Description = "Tarifa para uso industrial en media tensión",
                EffectiveFrom = effectiveDate,
                EffectiveTo = (DateTime?)null,
                IsActive = true
            }
        );
    }
}