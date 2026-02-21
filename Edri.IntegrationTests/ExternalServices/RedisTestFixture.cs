using System;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Infrastructure.Database;
using Edri.IntegrationTests.Fixtures;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.IntegrationTests.ExternalServices;

public sealed class RedisTestFixture : TestFixtureBase
{
    public Guid CreatedTenantId { get; } = Guid.NewGuid();
    
    public IDistributedCache DistributedCache { get; }

    public RedisTestFixture()
    {
        DistributedCache = Factory.Services.GetRequiredService<IDistributedCache>();
    }

    public async Task SeedTestData()
    {
        await GlobalSetupFixture.RespawnDatabaseAsync();

        using var context = Factory.Services.GetRequiredService<ApplicationDbContext>();

        context.Tenants.Add(new Tenant(
            CreatedTenantId,
            "Test Tenant"));

        await context.SaveChangesAsync();
    }
}