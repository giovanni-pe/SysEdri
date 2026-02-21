using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Branches");
        builder.HasKey(b => b.Id);

        // 2. Propiedades
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Address)
            .HasMaxLength(300);

        builder.Property(b => b.Phone)
            .HasMaxLength(20);

        builder.Property(b => b.IsActive)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(b => new { b.CompanyId, b.Name }).IsUnique();

        // 4. Relación con Company
        builder.HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. Relación con District
        builder.HasOne(b => b.District)
            .WithMany()
            .HasForeignKey(b => b.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Branches
        // ==============================================================================
        var electroOrienteId = Guid.Parse("e1000000-0000-0000-0000-000000000001");
        var electrocentroId = Guid.Parse("e1000000-0000-0000-0000-000000000002");
        var rupaRupaDistrictId = Guid.Parse("d7f8f0a1-0000-0000-0000-100601000001");

        builder.HasData(
            new
            {
                Id = Guid.Parse("f1000000-0000-0000-0000-000000000001"),
                CompanyId = electroOrienteId,
                DistrictId = rupaRupaDistrictId,
                Name = "Sucursal Tingo María",
                Address = "Av. Amazonas 456, Tingo María",
                Phone = "062561234",
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("f1000000-0000-0000-0000-000000000002"),
                CompanyId = electroOrienteId,
                DistrictId = rupaRupaDistrictId,
                Name = "Sucursal Centro",
                Address = "Jr. Raymondi 123, Tingo María",
                Phone = "062565678",
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("f1000000-0000-0000-0000-000000000003"),
                CompanyId = electrocentroId,
                DistrictId = rupaRupaDistrictId,
                Name = "Agencia Leoncio Prado",
                Address = "Av. Universitaria 789, Tingo María",
                Phone = "062569012",
                IsActive = true
            }
        );
    }
}