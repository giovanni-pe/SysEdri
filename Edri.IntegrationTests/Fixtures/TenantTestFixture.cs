using System;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.IntegrationTests.Fixtures;

public sealed class TenantTestFixture : TestFixtureBase
{
    public Guid CreatedTenantId { get; } = Guid.NewGuid();
    public Guid DeletedTenantId { get; } = Guid.NewGuid();

    public async Task SeedTestData()
    {
        await GlobalSetupFixture.RespawnDatabaseAsync();

        using var context = Factory.Services.GetRequiredService<ApplicationDbContext>();

        context.Tenants.Add(new Tenant(
            CreatedTenantId,
            "Test Tenant"));

        var deletedTenant = new Tenant(
            DeletedTenantId,
            "Test Tenant2");
        deletedTenant.Delete();
        context.Tenants.Add(deletedTenant);

        context.Users.Add(new User(
            Guid.NewGuid(),
            CreatedTenantId,
            "test@user.de",
            "test",
            "user",
            "Test User",
            UserRole.User));

        await context.SaveChangesAsync();
    }
}