using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Captures.GetAll;
using Edri.Application.Queries.Captures.GetCaptureById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;
using Edri.Domain;
using Edri.Domain.Commands.Captures.CreateCapture;
using Edri.Domain.Commands.Captures.DeleteCapture;
using Edri.Domain.Commands.Captures.UpdateCapture;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class CaptureService : ICaptureService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public CaptureService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateCaptureAsync(CreateCaptureViewModel capture)
    {
        var captureId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateCaptureCommand(
            captureId,
            capture.DeviceId,
            capture.Timestamp,
            capture.ImageUrl,
            capture.Status));

        return captureId;
    }

    public async Task UpdateCaptureAsync(UpdateCaptureViewModel capture)
    {
        await _bus.SendCommandAsync(new UpdateCaptureCommand(
            capture.Id,
            capture.DeviceId,
            capture.Timestamp,
            capture.ImageUrl,
            capture.Status));
    }

    public async Task DeleteCaptureAsync(Guid captureId)
    {
        await _bus.SendCommandAsync(new DeleteCaptureCommand(captureId));
    }

    public async Task<CaptureViewModel?> GetCaptureByIdAsync(Guid captureId)
    {
        var cachedCapture = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Capture>(captureId),
            async () => await _bus.QueryAsync(new GetCaptureByIdQuery(captureId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedCapture;
    }

    public async Task<PagedResult<CaptureViewModel>> GetAllCapturesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllCapturesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}