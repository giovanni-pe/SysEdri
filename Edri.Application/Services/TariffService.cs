using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Tariffs.GetAll;
using Edri.Application.Queries.Tariffs.GetTariffById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;
using Edri.Domain;
using Edri.Domain.Commands.Tariffs.CreateTariff;
using Edri.Domain.Commands.Tariffs.DeleteTariff;
using Edri.Domain.Commands.Tariffs.UpdateTariff;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class TariffService : ITariffService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public TariffService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateTariffAsync(CreateTariffViewModel tariff)
    {
        var tariffId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateTariffCommand(
            tariffId,
            tariff.CompanyId,
            tariff.Code,
            tariff.Name,
            tariff.PricePerKwh,
            tariff.FixedCharge,
            tariff.Description,
            tariff.EffectiveFrom,
            tariff.EffectiveTo,
            tariff.IsActive));

        return tariffId;
    }

    public async Task UpdateTariffAsync(UpdateTariffViewModel tariff)
    {
        await _bus.SendCommandAsync(new UpdateTariffCommand(
            tariff.Id,
            tariff.CompanyId,
            tariff.Code,
            tariff.Name,
            tariff.PricePerKwh,
            tariff.FixedCharge,
            tariff.Description,
            tariff.EffectiveFrom,
            tariff.EffectiveTo,
            tariff.IsActive));
    }

    public async Task DeleteTariffAsync(Guid tariffId)
    {
        await _bus.SendCommandAsync(new DeleteTariffCommand(tariffId));
    }

    public async Task<TariffViewModel?> GetTariffByIdAsync(Guid tariffId)
    {
        var cachedTariff = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Tariff>(tariffId),
            async () => await _bus.QueryAsync(new GetTariffByIdQuery(tariffId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedTariff;
    }

    public async Task<PagedResult<TariffViewModel>> GetAllTariffsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllTariffsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}