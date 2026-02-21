using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class SupplyConfiguration : IEntityTypeConfiguration<Supply>
{
    public void Configure(EntityTypeBuilder<Supply> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Supplies");
        builder.HasKey(s => s.Id);

        // 2. Propiedades
        builder.Property(s => s.SupplyNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.InstallationAddress)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(s => s.Reference)
            .HasMaxLength(200);

        builder.Property(s => s.Latitude)
            .HasPrecision(10, 7);

        builder.Property(s => s.Longitude)
            .HasPrecision(10, 7);

        builder.Property(s => s.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.ActivationDate)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(s => s.SupplyNumber).IsUnique();
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.CustomerId);

        // 4. Relación con Customer
        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. Relación con Tariff
        builder.HasOne(s => s.Tariff)
            .WithMany()
            .HasForeignKey(s => s.TariffId)
            .OnDelete(DeleteBehavior.Restrict);

        // 6. Relación con Branch
        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        // 7. Relación con District
        builder.HasOne(s => s.District)
            .WithMany()
            .HasForeignKey(s => s.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Supplies
        // ==============================================================================
        var customer1Id = Guid.Parse("b1000000-0000-0000-0000-000000000001");
        var customer2Id = Guid.Parse("b1000000-0000-0000-0000-000000000002");
        var customer3Id = Guid.Parse("b1000000-0000-0000-0000-000000000003");
        var customer4Id = Guid.Parse("b1000000-0000-0000-0000-000000000004");

        var tariffResidencial = Guid.Parse("aa000000-0000-0000-0000-000000000001");
        var tariffComercial = Guid.Parse("aa000000-0000-0000-0000-000000000003");
        var tariffIndustrial = Guid.Parse("aa000000-0000-0000-0000-000000000005");

        var branchTingoMaria = Guid.Parse("f1000000-0000-0000-0000-000000000001");
        var rupaRupaDistrictId = Guid.Parse("d7f8f0a1-0000-0000-0000-100601000001");

        var activationDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Suministro Residencial 1
            new
            {
                Id = Guid.Parse("d1000000-0000-0000-0000-000000000001"),
                SupplyNumber = "SUM-2025-000001",
                CustomerId = customer1Id,
                TariffId = tariffResidencial,
                BranchId = branchTingoMaria,
                DistrictId = rupaRupaDistrictId,
                InstallationAddress = "Av. Amazonas 123, Tingo María",
                Reference = "Frente al parque principal",
                Latitude = -9.2950m,
                Longitude = -76.0000m,
                Status = "Active",
                ActivationDate = activationDate,
                TerminationDate = (DateTime?)null
            },
            // Suministro Residencial 2
            new
            {
                Id = Guid.Parse("d1000000-0000-0000-0000-000000000002"),
                SupplyNumber = "SUM-2025-000002",
                CustomerId = customer2Id,
                TariffId = tariffResidencial,
                BranchId = branchTingoMaria,
                DistrictId = rupaRupaDistrictId,
                InstallationAddress = "Jr. Ucayali 456, Tingo María",
                Reference = "Cerca del mercado",
                Latitude = -9.2960m,
                Longitude = -76.0010m,
                Status = "Active",
                ActivationDate = activationDate,
                TerminationDate = (DateTime?)null
            },
            // Suministro Comercial
            new
            {
                Id = Guid.Parse("d1000000-0000-0000-0000-000000000003"),
                SupplyNumber = "SUM-2025-000003",
                CustomerId = customer3Id,
                TariffId = tariffComercial,
                BranchId = branchTingoMaria,
                DistrictId = rupaRupaDistrictId,
                InstallationAddress = "Av. Raymondi 789, Tingo María",
                Reference = "Local comercial esquina",
                Latitude = -9.2940m,
                Longitude = -75.9990m,
                Status = "Active",
                ActivationDate = activationDate,
                TerminationDate = (DateTime?)null
            },
            // Suministro Industrial
            new
            {
                Id = Guid.Parse("d1000000-0000-0000-0000-000000000004"),
                SupplyNumber = "SUM-2025-000004",
                CustomerId = customer4Id,
                TariffId = tariffIndustrial,
                BranchId = branchTingoMaria,
                DistrictId = rupaRupaDistrictId,
                InstallationAddress = "Carretera Central Km 5, Tingo María",
                Reference = "Zona industrial",
                Latitude = -9.2800m,
                Longitude = -75.9850m,
                Status = "Active",
                ActivationDate = activationDate,
                TerminationDate = (DateTime?)null
            }
        );
    }
}