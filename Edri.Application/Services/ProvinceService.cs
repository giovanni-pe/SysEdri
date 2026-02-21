using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Provinces.GetAll;
using Edri.Application.Queries.Provinces.GetProvinceById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;
using Edri.Domain;
using Edri.Domain.Commands.Provinces.CreateProvince;
using Edri.Domain.Commands.Provinces.DeleteProvince;
using Edri.Domain.Commands.Provinces.UpdateProvince;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class ProvinceService : IProvinceService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ProvinceService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateProvinceAsync(CreateProvinceViewModel Province)
    {
        var ProvinceId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateProvinceCommand(
            ProvinceId,
            Province.depatmentId,
            Province.Name,Province.Code));

        return ProvinceId;
    }

    public async Task UpdateProvinceAsync(UpdateProvinceViewModel Province)
    {
        await _bus.SendCommandAsync(new UpdateProvinceCommand(
            Province.Id,
            Province.depatmentId,
            Province.Name,Province.Code));
    }

    public async Task DeleteProvinceAsync(Guid ProvinceId)
    {
        await _bus.SendCommandAsync(new DeleteProvinceCommand(ProvinceId));
    }

    public async Task<ProvinceViewModel?> GetProvinceByIdAsync(Guid ProvinceId)
    {
        var cachedProvince = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Province>(ProvinceId),
            async () => await _bus.QueryAsync(new GetProvinceByIdQuery(ProvinceId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedProvince;
    }

    public async Task<PagedResult<ProvinceViewModel>> GetAllProvincesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllProvincesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}