using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class MeterConfiguration : IEntityTypeConfiguration<Meter>
{
    public void Configure(EntityTypeBuilder<Meter> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Meters");
        builder.HasKey(m => m.Id);

        // 2. Propiedades
        builder.Property(m => m.MeterNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Brand)
            .HasMaxLength(50);

        builder.Property(m => m.Model)
            .HasMaxLength(50);

        builder.Property(m => m.Type)
            .IsRequired();

        builder.Property(m => m.Status)
            .IsRequired();

        builder.Property(m => m.InstallationDate)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(m => m.MeterNumber).IsUnique();
        builder.HasIndex(m => m.SupplyId);
        builder.HasIndex(m => m.Status);

        // 4. Relación con Supply
        builder.HasOne(m => m.Supply)
            .WithMany()
            .HasForeignKey(m => m.SupplyId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Meters
        // ==============================================================================
        var supply1Id = Guid.Parse("d1000000-0000-0000-0000-000000000001");
        var supply2Id = Guid.Parse("d1000000-0000-0000-0000-000000000002");
        var supply3Id = Guid.Parse("d1000000-0000-0000-0000-000000000003");
        var supply4Id = Guid.Parse("d1000000-0000-0000-0000-000000000004");

        var installationDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var calibrationDate = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Medidor Analógico - Suministro Residencial 1
            new
            {
                Id = Guid.Parse("c2000000-0000-0000-0000-000000000001"),
                MeterNumber = "MED-2025-000001",
                SupplyId = supply1Id,
                Brand = "ELSTER",
                Model = "A1100",
                Type = 1, // Analog
                AmperageCapacity = (int?)30,
                InstallationDate = installationDate,
                LastCalibrationDate = (DateTime?)calibrationDate,
                Status = 1 // Active
            },
            // Medidor Digital - Suministro Residencial 2
            new
            {
                Id = Guid.Parse("c2000000-0000-0000-0000-000000000002"),
                MeterNumber = "MED-2025-000002",
                SupplyId = supply2Id,
                Brand = "LANDIS+GYR",
                Model = "E350",
                Type = 2, // Digital
                AmperageCapacity = (int?)40,
                InstallationDate = installationDate,
                LastCalibrationDate = (DateTime?)calibrationDate,
                Status = 1 // Active
            },
            // Medidor Smart - Suministro Comercial
            new
            {
                Id = Guid.Parse("c2000000-0000-0000-0000-000000000003"),
                MeterNumber = "MED-2025-000003",
                SupplyId = supply3Id,
                Brand = "ITRON",
                Model = "OpenWay Riva",
                Type = 3, // Smart
                AmperageCapacity = (int?)60,
                InstallationDate = installationDate,
                LastCalibrationDate = (DateTime?)calibrationDate,
                Status = 1 // Active
            },
            // Medidor Smart - Suministro Industrial
            new
            {
                Id = Guid.Parse("c2000000-0000-0000-0000-000000000004"),
                MeterNumber = "MED-2025-000004",
                SupplyId = supply4Id,
                Brand = "SCHNEIDER",
                Model = "ION8650",
                Type = 3, // Smart
                AmperageCapacity = (int?)100,
                InstallationDate = installationDate,
                LastCalibrationDate = (DateTime?)calibrationDate,
                Status = 1 // Active
            }
        );
    }
}