using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Devices");
        builder.HasKey(d => d.Id);

        // 2. Propiedades
        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.MacAddress)
            .HasMaxLength(17);

        builder.Property(d => d.Model)
            .HasMaxLength(100);

        builder.Property(d => d.FirmwareVersion)
            .HasMaxLength(50);

        builder.Property(d => d.Status)
            .IsRequired();

        builder.Property(d => d.InstallationDate)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(d => d.Code).IsUnique();
        builder.HasIndex(d => d.MeterId);
        builder.HasIndex(d => d.BranchId);
        builder.HasIndex(d => d.Status);

        // 4. Relación con Meter
        builder.HasOne(d => d.Meter)
            .WithMany()
            .HasForeignKey(d => d.MeterId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. Relación con Branch
        builder.HasOne(d => d.Branch)
            .WithMany()
            .HasForeignKey(d => d.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Devices (IoT para captura OCR de medidores)
        // ==============================================================================
        var meter1Id = Guid.Parse("c2000000-0000-0000-0000-000000000001");
        var meter2Id = Guid.Parse("c2000000-0000-0000-0000-000000000002");
        var meter3Id = Guid.Parse("c2000000-0000-0000-0000-000000000003");
        var meter4Id = Guid.Parse("c2000000-0000-0000-0000-000000000004");

        var branchTingoMaria = Guid.Parse("f1000000-0000-0000-0000-000000000001");

        var installationDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc);
        var lastConnection = new DateTime(2025, 1, 20, 10, 30, 0, DateTimeKind.Utc);

        builder.HasData(
            // Dispositivo IoT para Medidor Residencial 1
            new
            {
                Id = Guid.Parse("de000000-0000-0000-0000-000000000001"),
                Code = "IOT-TM-001",
                MeterId = meter1Id,
                BranchId = branchTingoMaria,
                MacAddress = "AA:BB:CC:DD:EE:01",
                Model = "ESP32-CAM",
                FirmwareVersion = "1.0.0",
                Status = 1, // Active
                LastConnection = (DateTime?)lastConnection,
                InstallationDate = installationDate
            },
            // Dispositivo IoT para Medidor Residencial 2
            new
            {
                Id = Guid.Parse("de000000-0000-0000-0000-000000000002"),
                Code = "IOT-TM-002",
                MeterId = meter2Id,
                BranchId = branchTingoMaria,
                MacAddress = "AA:BB:CC:DD:EE:02",
                Model = "ESP32-CAM",
                FirmwareVersion = "1.0.0",
                Status = 1, // Active
                LastConnection = (DateTime?)lastConnection,
                InstallationDate = installationDate
            },
            // Dispositivo IoT para Medidor Comercial
            new
            {
                Id = Guid.Parse("de000000-0000-0000-0000-000000000003"),
                Code = "IOT-TM-003",
                MeterId = meter3Id,
                BranchId = branchTingoMaria,
                MacAddress = "AA:BB:CC:DD:EE:03",
                Model = "Raspberry Pi 4",
                FirmwareVersion = "2.1.0",
                Status = 1, // Active
                LastConnection = (DateTime?)lastConnection,
                InstallationDate = installationDate
            },
            // Dispositivo IoT para Medidor Industrial (en mantenimiento)
            new
            {
                Id = Guid.Parse("de000000-0000-0000-0000-000000000004"),
                Code = "IOT-TM-004",
                MeterId = meter4Id,
                BranchId = branchTingoMaria,
                MacAddress = "AA:BB:CC:DD:EE:04",
                Model = "Raspberry Pi 4",
                FirmwareVersion = "2.1.0",
                Status = 3, // Maintenance
                LastConnection = (DateTime?)null,
                InstallationDate = installationDate
            }
        );
    }
}