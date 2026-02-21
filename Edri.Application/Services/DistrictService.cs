using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Districts.GetAll;
using Edri.Application.Queries.Districts.GetDistrictById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;
using Edri.Domain;
using Edri.Domain.Commands.Districts.CreateDistrict;
using Edri.Domain.Commands.Districts.DeleteDistrict;
using Edri.Domain.Commands.Districts.UpdateDistrict;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class DistrictService : IDistrictService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public DistrictService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateDistrictAsync(CreateDistrictViewModel District)
    {
        var DistrictId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateDistrictCommand(
            DistrictId,
            District.provinceId,
            District.Name,District.Code));

        return DistrictId;
    }

    public async Task UpdateDistrictAsync(UpdateDistrictViewModel District)
    {
        await _bus.SendCommandAsync(new UpdateDistrictCommand(
            District.Id,
            District.provinceId,
            District.Name,District.Code));
    }

    public async Task DeleteDistrictAsync(Guid DistrictId)
    {
        await _bus.SendCommandAsync(new DeleteDistrictCommand(DistrictId));
    }

    public async Task<DistrictViewModel?> GetDistrictByIdAsync(Guid DistrictId)
    {
        var cachedDistrict = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<District>(DistrictId),
            async () => await _bus.QueryAsync(new GetDistrictByIdQuery(DistrictId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedDistrict;
    }

    public async Task<PagedResult<DistrictViewModel>> GetAllDistrictsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllDistrictsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}