using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Capture;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class CaptureEventHandler :
    INotificationHandler<CaptureCreatedEvent>,
    INotificationHandler<CaptureDeletedEvent>,
    INotificationHandler<CaptureUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public CaptureEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(CaptureCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(CaptureDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Capture>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(CaptureUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Capture>(notification.AggregateId),
            cancellationToken);
    }
}