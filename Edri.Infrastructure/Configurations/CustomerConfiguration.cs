using System;
using Edri.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // 1. Configuración de la Tabla
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);

        // 2. Propiedades
        builder.Property(c => c.DocumentType)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(c => c.DocumentNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.CustomerType)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.BusinessName)
            .HasMaxLength(200);

        builder.Property(c => c.FirstName)
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasMaxLength(100);

        builder.Property(c => c.Address)
            .HasMaxLength(300);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.RegistrationDate)
            .IsRequired();

        // 3. Índices
        builder.HasIndex(c => c.DocumentNumber).IsUnique();
        builder.HasIndex(c => c.Email);
        builder.HasIndex(c => c.Status);

        // 4. Relación con User
        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. Relación con District
        builder.HasOne(c => c.District)
            .WithMany()
            .HasForeignKey(c => c.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==============================================================================
        // SEED DATA: Customers (usando objetos anónimos para evitar DateTime.UtcNow)
        // ==============================================================================
        var rupaRupaDistrictId = Guid.Parse("d7f8f0a1-0000-0000-0000-100601000001");
        var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            // Cliente Residencial 1
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000001"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000001"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "DNI",
                DocumentNumber = "12345678",
                CustomerType = "Residential",
                FirstName = "Juan",
                LastName = "Pérez García",
                Address = "Av. Amazonas 123, Tingo María",
                Phone = "962123456",
                Email = "juan.perez@email.com",
                Status = "Active",
                RegistrationDate = seedDate
            },

            // Cliente Residencial 2
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000002"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000002"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "DNI",
                DocumentNumber = "87654321",
                CustomerType = "Residential",
                FirstName = "María",
                LastName = "López Rodríguez",
                Address = "Jr. Ucayali 456, Tingo María",
                Phone = "962654321",
                Email = "maria.lopez@email.com",
                Status = "Active",
                RegistrationDate = seedDate
            },

            // Cliente Comercial
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000003"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000003"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "RUC",
                DocumentNumber = "20123456789",
                CustomerType = "Commercial",
                BusinessName = "Comercial Los Andes S.A.C.",
                Address = "Av. Raymondi 789, Tingo María",
                Phone = "062562000",
                Email = "contacto@losandes.com",
                Status = "Active",
                RegistrationDate = seedDate
            },

            // Cliente Industrial
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000004"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000004"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "RUC",
                DocumentNumber = "20987654321",
                CustomerType = "Industrial",
                BusinessName = "Agroindustrias del Oriente S.A.",
                Address = "Carretera Central Km 5, Tingo María",
                Phone = "062563000",
                Email = "info@agroindustriasoriente.com",
                Status = "Active",
                RegistrationDate = seedDate
            },

            // Cliente Suspendido
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000005"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000005"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "DNI",
                DocumentNumber = "45678912",
                CustomerType = "Residential",
                FirstName = "Carlos",
                LastName = "Mendoza Silva",
                Address = "Jr. Huánuco 321, Tingo María",
                Phone = "962789456",
                Email = "carlos.mendoza@email.com",
                Status = "Suspended",
                RegistrationDate = seedDate
            },

            // Cliente con Carnet de Extranjería
            new
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000006"),
                UserId = Guid.Parse("a1000000-0000-0000-0000-000000000006"),
                DistrictId = rupaRupaDistrictId,
                DocumentType = "CE",
                DocumentNumber = "CE123456",
                CustomerType = "Residential",
                FirstName = "Roberto",
                LastName = "González Martínez",
                Address = "Av. Universitaria 555, Tingo María",
                Phone = "962111222",
                Email = "roberto.gonzalez@email.com",
                Status = "Active",
                RegistrationDate = seedDate
            }
        );
    }
}