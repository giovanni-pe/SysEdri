using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Tenants.GetAll;
using Edri.Application.Queries.Tenants.GetTenantById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;
using Edri.Domain;
using Edri.Domain.Commands.Tenants.CreateTenant;
using Edri.Domain.Commands.Tenants.DeleteTenant;
using Edri.Domain.Commands.Tenants.UpdateTenant;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class TenantService : ITenantService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public TenantService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateTenantAsync(CreateTenantViewModel tenant)
    {
        var tenantId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateTenantCommand(
            tenantId,
            tenant.Name));

        return tenantId;
    }

    public async Task UpdateTenantAsync(UpdateTenantViewModel tenant)
    {
        await _bus.SendCommandAsync(new UpdateTenantCommand(
            tenant.Id,
            tenant.Name));
    }

    public async Task DeleteTenantAsync(Guid tenantId)
    {
        await _bus.SendCommandAsync(new DeleteTenantCommand(tenantId));
    }

    public async Task<TenantViewModel?> GetTenantByIdAsync(Guid tenantId)
    {
        var cachedTenant = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Tenant>(tenantId),
            async () => await _bus.QueryAsync(new GetTenantByIdQuery(tenantId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedTenant;
    }

    public async Task<PagedResult<TenantViewModel>> GetAllTenantsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllTenantsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}