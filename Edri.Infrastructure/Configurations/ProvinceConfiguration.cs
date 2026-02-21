using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Provinces");
        builder.HasKey(p => p.Id);

        // 2. Propiedades
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(2) // Ej: "01"
            .IsFixedLength();

        // 3. Relación con Department
        builder.HasOne(p => p.Department)
            .WithMany() // O .WithMany(d => d.Provinces) si agregaste la colección en Department
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // 4. Índice Único Compuesto (DepartmentId + Code)
        // Esto permite que Huánuco tenga la prov "01" y Lima también tenga la prov "01".
        builder.HasIndex(p => new { p.DepartmentId, p.Code }).IsUnique();

        // ==============================================================================
        // SEED DATA: Provincias de Huánuco (Department Code: 10)
        // ID del Dept. Huánuco (del paso anterior): d7f8f0a1-1b1a-4b1a-8b1a-000000000010
        // ==============================================================================

        var huanucoId = Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000010");

        builder.HasData(
            // 1001: Huánuco
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000001"), huanucoId, "Huánuco", "01"),

            // 1002: Ambo
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000002"), huanucoId, "Ambo", "02"),

            // 1003: Dos de Mayo
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000003"), huanucoId, "Dos de Mayo", "03"),

            // 1004: Huacaybamba
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000004"), huanucoId, "Huacaybamba", "04"),

            // 1005: Huamalíes
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000005"), huanucoId, "Huamalíes", "05"),

            // 1006: Leoncio Prado
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000006"), huanucoId, "Leoncio Prado", "06"),

            // 1007: Marañón
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000007"), huanucoId, "Marañón", "07"),

            // 1008: Pachitea
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000008"), huanucoId, "Pachitea", "08"),

            // 1009: Puerto Inca
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000009"), huanucoId, "Puerto Inca", "09"),

            // 1010: Lauricocha
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000010"), huanucoId, "Lauricocha", "10"),

            // 1011: Yarowilca
            new Province(Guid.Parse("a1b2c3d4-0000-0000-0000-100000000011"), huanucoId, "Yarowilca", "11")
        );
    }
}