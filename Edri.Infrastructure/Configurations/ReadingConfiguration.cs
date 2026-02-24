using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class ReadingConfiguration : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Readings");
        builder.HasKey(r => r.Id);

        // 2. Propiedades
        builder.Property(r => r.MeterNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.ValueKwh)
            .IsRequired()
            .HasPrecision(18, 4);

        builder.Property(r => r.ReadingDate)
            .IsRequired();

        builder.Property(r => r.Source)
            .IsRequired();

        builder.Property(r => r.OcrConfidence)
            .HasPrecision(5, 2);

        builder.Property(r => r.Observations)
            .HasMaxLength(500);

        // 3. Índices para consultas de alta velocidad
        builder.HasIndex(r => r.MeterNumber);
        builder.HasIndex(r => r.ReadingDate);
        builder.HasIndex(r => r.DeviceId);
        builder.HasIndex(r => new { r.MeterNumber, r.ReadingDate });

        // 4. Relación con Device
        builder.HasOne(r => r.Device)
            .WithMany()
            .HasForeignKey(r => r.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. Relación con Capture (opcional)
        builder.HasOne(r => r.Capture)
            .WithMany()
            .HasForeignKey(r => r.CaptureId)
            .OnDelete(DeleteBehavior.SetNull);

        // ==============================================================================
        // SEED DATA: Readings (Lecturas generadas por OCR y manuales)
        // ==============================================================================
        var device1Id = Guid.Parse("de000000-0000-0000-0000-000000000001");
        var device2Id = Guid.Parse("de000000-0000-0000-0000-000000000002");
        var device3Id = Guid.Parse("de000000-0000-0000-0000-000000000003");

        var capture1Id = Guid.Parse("ca000000-0000-0000-0000-000000000001");
        var capture2Id = Guid.Parse("ca000000-0000-0000-0000-000000000002");
        var capture3Id = Guid.Parse("ca000000-0000-0000-0000-000000000003");
        var capture5Id = Guid.Parse("ca000000-0000-0000-0000-000000000005");

        var readingDate1 = new DateTime(2025, 1, 15, 8, 0, 0, DateTimeKind.Utc);
        var readingDate2 = new DateTime(2025, 1, 20, 8, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Lecturas OCR del Medidor 1 (Residencial)
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000001"),
                DeviceId = device1Id,
                CaptureId = (Guid?)capture1Id,
                MeterNumber = "MED-2025-000001",
                ValueKwh = 1250.50m,
                ReadingDate = readingDate1,
                Source = 1, // OCR
                OcrConfidence = (decimal?)98.5m,
                Observations = (string?)null
            },
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000002"),
                DeviceId = device1Id,
                CaptureId = (Guid?)capture2Id,
                MeterNumber = "MED-2025-000001",
                ValueKwh = 1320.75m,
                ReadingDate = readingDate2,
                Source = 1, // OCR
                OcrConfidence = (decimal?)99.1m,
                Observations = (string?)null
            },
            // Lecturas OCR del Medidor 2 (Residencial)
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000003"),
                DeviceId = device2Id,
                CaptureId = (Guid?)capture3Id,
                MeterNumber = "MED-2025-000002",
                ValueKwh = 980.25m,
                ReadingDate = readingDate1,
                Source = 1, // OCR
                OcrConfidence = (decimal?)97.8m,
                Observations = (string?)null
            },
            // Lectura OCR del Medidor 3 (Comercial)
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000004"),
                DeviceId = device3Id,
                CaptureId = (Guid?)capture5Id,
                MeterNumber = "MED-2025-000003",
                ValueKwh = 5420.00m,
                ReadingDate = readingDate1,
                Source = 1, // OCR
                OcrConfidence = (decimal?)99.5m,
                Observations = (string?)null
            },
            // Lectura Manual del Medidor 4 (Industrial - dispositivo en mantenimiento)
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000005"),
                DeviceId = Guid.Parse("de000000-0000-0000-0000-000000000004"),
                CaptureId = (Guid?)null,
                MeterNumber = "MED-2025-000004",
                ValueKwh = 15800.00m,
                ReadingDate = readingDate1,
                Source = 2, // Manual
                OcrConfidence = (decimal?)null,
                Observations = "Lectura manual - dispositivo en mantenimiento"
            },
            // Lectura Estimada del Medidor 2 (no llegó captura del 20/01)
            new
            {
                Id = Guid.Parse("ae000000-0000-0000-0000-000000000006"),
                DeviceId = device2Id,
                CaptureId = (Guid?)null,
                MeterNumber = "MED-2025-000002",
                ValueKwh = 1045.00m,
                ReadingDate = readingDate2,
                Source = 3, // Estimated
                OcrConfidence = (decimal?)null,
                Observations = "Lectura estimada basada en consumo promedio histórico"
            }
        );
    }
}