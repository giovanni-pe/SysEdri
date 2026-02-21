using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edri.gRPC.Interfaces;
using Edri.Proto.Tenants;
using Edri.Shared.Tenants;

namespace Edri.gRPC.Contexts;

public sealed class TenantsContext : ITenantsContext
{
    private readonly TenantsApi.TenantsApiClient _client;

    public TenantsContext(TenantsApi.TenantsApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<TenantViewModel>> GetTenantsByIds(IEnumerable<Guid> ids)
    {
        var request = new GetTenantsByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Tenants.Select(tenant => new TenantViewModel(
            Guid.Parse(tenant.Id),
            tenant.Name,
            string.IsNullOrWhiteSpace(tenant.DeletedAt) ? null : DateTimeOffset.Parse(tenant.DeletedAt)));
    }
}