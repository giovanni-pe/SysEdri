using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Districts");
        builder.HasKey(d => d.Id);

        // 2. Propiedades
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        // EL CODE ES EL UBIGEO COMPLETO (6 dígitos)
        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(6)
            .IsFixedLength();

        // 3. Índices
        // El Ubigeo es único en todo el país
        builder.HasIndex(d => d.Code).IsUnique();

        // 4. Relación con Provincia
        builder.HasOne(d => d.Province)
            .WithMany()
            .HasForeignKey(d => d.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Distritos de Leoncio Prado (Huánuco) - Código Prov: 1006
        // ==============================================================================

        // Este ID debe coincidir EXACTAMENTE con el que pusiste en ProvinceConfiguration para "Leoncio Prado"
        var leoncioPradoId = Guid.Parse("a1b2c3d4-0000-0000-0000-100000000006");

        builder.HasData(
            // 100601: Rupa-Rupa (Capital: Tingo María)
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100601000001"), leoncioPradoId, "Rupa-Rupa", "100601"),

            // 100602: Daniel Alomía Robles
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100602000002"), leoncioPradoId, "Daniel Alomía Robles", "100602"),

            // 100603: Hermilio Valdizán
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100603000003"), leoncioPradoId, "Hermilio Valdizán", "100603"),

            // 100604: José Crespo y Castillo
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100604000004"), leoncioPradoId, "José Crespo y Castillo", "100604"),

            // 100605: Luyando
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100605000005"), leoncioPradoId, "Luyando", "100605"),

            // 100606: Mariano Dámaso Beraún
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100606000006"), leoncioPradoId, "Mariano Dámaso Beraún", "100606"),

            // 100607: Pucayacu (Creado en 2015)
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100607000007"), leoncioPradoId, "Pucayacu", "100607"),

            // 100608: Castillo Grande (Creado en 2015)
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100608000008"), leoncioPradoId, "Castillo Grande", "100608"),

            // 100609: Pueblo Nuevo (Creado en 2015)
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100609000009"), leoncioPradoId, "Pueblo Nuevo", "100609"),

            // 100610: Santo Domingo de Anda (Creado en 2015)
            new District(Guid.Parse("d7f8f0a1-0000-0000-0000-100610000010"), leoncioPradoId, "Santo Domingo de Anda", "100610")
        );
    }
}