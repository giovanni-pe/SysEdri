using System;
using Edri.Domain.Constants; // Asegúrate de tener aquí MaxLengths.Department
using Edri.Domain.Entities;  // O Edri.Domain.Entities.Geography
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // Configuración de la Tabla
        builder.ToTable("Departments");

        builder.HasKey(t => t.Id);

        // Propiedad Name
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100); // O pon 100 si no tienes la constante

        // Propiedad Code (Ubigeo - Ej: "15")
        builder.Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(2)
            .IsFixedLength(); // CHAR(2) es más eficiente que VARCHAR

        // El código debe ser único en la base de datos
        builder.HasIndex(t => t.Code).IsUnique();

        // SEED DATA: Departamentos del Perú (INEI)
        // Nota: He generado Guids estáticos para evitar que cambien en cada migración.
        builder.HasData(
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000001"), "Amazonas", "01"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000002"), "Áncash", "02"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000003"), "Apurímac", "03"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000004"), "Arequipa", "04"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000005"), "Ayacucho", "05"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000006"), "Cajamarca", "06"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000007"), "Callao", "07"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000008"), "Cusco", "08"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000009"), "Huancavelica", "09"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), "Huánuco", "10"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000011"), "Ica", "11"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000012"), "Junín", "12"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000013"), "La Libertad", "13"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000014"), "Lambayeque", "14"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000015"), "Lima", "15"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000016"), "Loreto", "16"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000017"), "Madre de Dios", "17"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000018"), "Moquegua", "18"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000019"), "Pasco", "19"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000020"), "Piura", "20"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000021"), "Puno", "21"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000022"), "San Martín", "22"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000023"), "Tacna", "23"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000024"), "Tumbes", "24"),
            new Department(Guid.Parse("d7f8f0a1-1b1a-4b1a-8b1a-000000000025"), "Ucayali", "25")
        );
    }
}