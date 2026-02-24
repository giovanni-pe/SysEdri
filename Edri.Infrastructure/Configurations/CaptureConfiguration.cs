using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class CaptureConfiguration : IEntityTypeConfiguration<Capture>
{
    public void Configure(EntityTypeBuilder<Capture> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Captures");
        builder.HasKey(c => c.Id);

        // 2. Propiedades
        builder.Property(c => c.Timestamp)
            .IsRequired();

        builder.Property(c => c.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Status)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(c => c.DeviceId);
        builder.HasIndex(c => c.Timestamp);
        builder.HasIndex(c => c.Status);

        // 4. Relación con Device
        builder.HasOne(c => c.Device)
            .WithMany()
            .HasForeignKey(c => c.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Captures (Imágenes capturadas por dispositivos IoT para OCR)
        // ==============================================================================
        var device1Id = Guid.Parse("de000000-0000-0000-0000-000000000001");
        var device2Id = Guid.Parse("de000000-0000-0000-0000-000000000002");
        var device3Id = Guid.Parse("de000000-0000-0000-0000-000000000003");

        var captureDate1 = new DateTime(2025, 1, 15, 8, 0, 0, DateTimeKind.Utc);
        var captureDate2 = new DateTime(2025, 1, 15, 8, 5, 0, DateTimeKind.Utc);
        var captureDate3 = new DateTime(2025, 1, 15, 8, 10, 0, DateTimeKind.Utc);
        var captureDate4 = new DateTime(2025, 1, 20, 8, 0, 0, DateTimeKind.Utc);
        var captureDate5 = new DateTime(2025, 1, 20, 8, 5, 0, DateTimeKind.Utc);

        builder.HasData(
            // Capturas del Dispositivo 1 - Procesadas exitosamente
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000001"),
                DeviceId = device1Id,
                Timestamp = captureDate1,
                ImageUrl = "/captures/2025/01/15/IOT-TM-001_080000.jpg",
                Status = 2 // Processed
            },
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000002"),
                DeviceId = device1Id,
                Timestamp = captureDate4,
                ImageUrl = "/captures/2025/01/20/IOT-TM-001_080000.jpg",
                Status = 2 // Processed
            },
            // Capturas del Dispositivo 2 - Una procesada, una pendiente
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000003"),
                DeviceId = device2Id,
                Timestamp = captureDate2,
                ImageUrl = "/captures/2025/01/15/IOT-TM-002_080500.jpg",
                Status = 2 // Processed
            },
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000004"),
                DeviceId = device2Id,
                Timestamp = captureDate5,
                ImageUrl = "/captures/2025/01/20/IOT-TM-002_080500.jpg",
                Status = 1 // Pending
            },
            // Capturas del Dispositivo 3 - Una procesada, una fallida
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000005"),
                DeviceId = device3Id,
                Timestamp = captureDate3,
                ImageUrl = "/captures/2025/01/15/IOT-TM-003_081000.jpg",
                Status = 2 // Processed
            },
            new
            {
                Id = Guid.Parse("ca000000-0000-0000-0000-000000000006"),
                DeviceId = device3Id,
                Timestamp = captureDate4,
                ImageUrl = "/captures/2025/01/20/IOT-TM-003_080000.jpg",
                Status = 3 // Failed (imagen borrosa, OCR no pudo leer)
            }
        );
    }
}