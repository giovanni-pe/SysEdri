using System;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Infrastructure.Database;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.IntegrationTests.Fixtures.gRPC;

public sealed class GetTenantsByIdsTestFixture : TestFixtureBase
{
    public GrpcChannel GrpcChannel { get; }
    public Guid CreatedTenantId { get; } = Guid.NewGuid();

    public GetTenantsByIdsTestFixture()
    {
        GrpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Factory.Server.CreateHandler()
        });
    }

    public async Task SeedTestData()
    {
        await GlobalSetupFixture.RespawnDatabaseAsync();

        using var context = Factory.Services.GetRequiredService<ApplicationDbContext>();

        var tenant = CreateTenant();
        tenant.Delete();

        context.Tenants.Add(tenant);
        await context.SaveChangesAsync();
    }

    public Tenant CreateTenant()
    {
        return new Tenant(
            CreatedTenantId,
            "Test Tenant");
    }
}