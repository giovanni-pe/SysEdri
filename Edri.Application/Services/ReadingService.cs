using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Readings.GetAll;
using Edri.Application.Queries.Readings.GetReadingById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;
using Edri.Domain;
using Edri.Domain.Commands.Readings.CreateReading;
using Edri.Domain.Commands.Readings.DeleteReading;
using Edri.Domain.Commands.Readings.UpdateReading;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class ReadingService : IReadingService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ReadingService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateReadingAsync(CreateReadingViewModel reading)
    {
        var readingId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateReadingCommand(
            readingId,
            reading.DeviceId,
            reading.CaptureId,
            reading.MeterNumber,
            reading.ValueKwh,
            reading.ReadingDate,
            reading.Source,
            reading.OcrConfidence,
            reading.Observations));

        return readingId;
    }

    public async Task UpdateReadingAsync(UpdateReadingViewModel reading)
    {
        await _bus.SendCommandAsync(new UpdateReadingCommand(
            reading.Id,
            reading.DeviceId,
            reading.CaptureId,
            reading.MeterNumber,
            reading.ValueKwh,
            reading.ReadingDate,
            reading.Source,
            reading.OcrConfidence,
            reading.Observations));
    }

    public async Task DeleteReadingAsync(Guid readingId)
    {
        await _bus.SendCommandAsync(new DeleteReadingCommand(readingId));
    }

    public async Task<ReadingViewModel?> GetReadingByIdAsync(Guid readingId)
    {
        var cachedReading = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Reading>(readingId),
            async () => await _bus.QueryAsync(new GetReadingByIdQuery(readingId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedReading;
    }

    public async Task<PagedResult<ReadingViewModel>> GetAllReadingsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllReadingsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}