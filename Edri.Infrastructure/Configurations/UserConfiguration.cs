using System;
using Edri.Domain.Constants;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edri.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Email);

        builder
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.FirstName);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.LastName);

        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Password);

        // ==============================================================================
        // SEED DATA: Usuarios
        // ==============================================================================
        // Password para todos: !Password123#
        var passwordHash = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2";

        builder.HasData(
            // Admin
            new User(
                Ids.Seed.UserId,
                Ids.Seed.TenantId,
                "admin@email.com",
                "Admin",
                "User",
                passwordHash,
                UserRole.Admin),

            // Customer Users
            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000001"),
                Ids.Seed.TenantId,
                "juan.perez@email.com",
                "Juan",
                "Pérez García",
                passwordHash,
                UserRole.Customer),

            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000002"),
                Ids.Seed.TenantId,
                "maria.lopez@email.com",
                "María",
                "López Rodríguez",
                passwordHash,
                UserRole.Customer),

            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000003"),
                Ids.Seed.TenantId,
                "contacto@losandes.com",
                "Comercial",
                "Los Andes",
                passwordHash,
                UserRole.Customer),

            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000004"),
                Ids.Seed.TenantId,
                "info@agroindustriasoriente.com",
                "Agroindustrias",
                "del Oriente",
                passwordHash,
                UserRole.Customer),

            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000005"),
                Ids.Seed.TenantId,
                "carlos.mendoza@email.com",
                "Carlos",
                "Mendoza Silva",
                passwordHash,
                UserRole.Customer),

            new User(
                Guid.Parse("a1000000-0000-0000-0000-000000000006"),
                Ids.Seed.TenantId,
                "roberto.gonzalez@email.com",
                "Roberto",
                "González Martínez",
                passwordHash,
                UserRole.Customer)
        );
    }
}