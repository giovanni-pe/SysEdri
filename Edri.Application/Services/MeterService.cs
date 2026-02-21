using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Meters.GetAll;
using Edri.Application.Queries.Meters.GetMeterById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;
using Edri.Domain;
using Edri.Domain.Commands.Meters.CreateMeter;
using Edri.Domain.Commands.Meters.DeleteMeter;
using Edri.Domain.Commands.Meters.UpdateMeter;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class MeterService : IMeterService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public MeterService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateMeterAsync(CreateMeterViewModel meter)
    {
        var meterId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateMeterCommand(
            meterId,
            meter.MeterNumber,
            meter.SupplyId,
            meter.Brand,
            meter.Model,
            meter.Type,
            meter.AmperageCapacity,
            meter.InstallationDate,
            meter.LastCalibrationDate,
            meter.Status));

        return meterId;
    }

    public async Task UpdateMeterAsync(UpdateMeterViewModel meter)
    {
        await _bus.SendCommandAsync(new UpdateMeterCommand(
            meter.Id,
            meter.MeterNumber,
            meter.SupplyId,
            meter.Brand,
            meter.Model,
            meter.Type,
            meter.AmperageCapacity,
            meter.InstallationDate,
            meter.LastCalibrationDate,
            meter.Status));
    }

    public async Task DeleteMeterAsync(Guid meterId)
    {
        await _bus.SendCommandAsync(new DeleteMeterCommand(meterId));
    }

    public async Task<MeterViewModel?> GetMeterByIdAsync(Guid meterId)
    {
        var cachedMeter = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Meter>(meterId),
            async () => await _bus.QueryAsync(new GetMeterByIdQuery(meterId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedMeter;
    }

    public async Task<PagedResult<MeterViewModel>> GetAllMetersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllMetersQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}