using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Supplies.GetAll;
using Edri.Application.Queries.Supplies.GetSupplyById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;
using Edri.Domain;
using Edri.Domain.Commands.Supplies.CreateSupply;
using Edri.Domain.Commands.Supplies.DeleteSupply;
using Edri.Domain.Commands.Supplies.UpdateSupply;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class SupplyService : ISupplyService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public SupplyService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateSupplyAsync(CreateSupplyViewModel supply)
    {
        var supplyId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateSupplyCommand(
            supplyId,
            supply.SupplyNumber,
            supply.CustomerId,
            supply.TariffId,
            supply.BranchId,
            supply.DistrictId,
            supply.InstallationAddress,
            supply.Reference,
            supply.Latitude,
            supply.Longitude,
            supply.Status,
            supply.ActivationDate,
            supply.TerminationDate));

        return supplyId;
    }

    public async Task UpdateSupplyAsync(UpdateSupplyViewModel supply)
    {
        await _bus.SendCommandAsync(new UpdateSupplyCommand(
            supply.Id,
            supply.SupplyNumber,
            supply.CustomerId,
            supply.TariffId,
            supply.BranchId,
            supply.DistrictId,
            supply.InstallationAddress,
            supply.Reference,
            supply.Latitude,
            supply.Longitude,
            supply.Status,
            supply.ActivationDate,
            supply.TerminationDate));
    }

    public async Task DeleteSupplyAsync(Guid supplyId)
    {
        await _bus.SendCommandAsync(new DeleteSupplyCommand(supplyId));
    }

    public async Task<SupplyViewModel?> GetSupplyByIdAsync(Guid supplyId)
    {
        var cachedSupply = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Supply>(supplyId),
            async () => await _bus.QueryAsync(new GetSupplyByIdQuery(supplyId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedSupply;
    }

    public async Task<PagedResult<SupplyViewModel>> GetAllSuppliesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllSuppliesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}