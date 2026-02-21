using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Meter;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class MeterEventHandler :
    INotificationHandler<MeterCreatedEvent>,
    INotificationHandler<MeterDeletedEvent>,
    INotificationHandler<MeterUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public MeterEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(MeterCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(MeterDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Meter>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(MeterUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Meter>(notification.AggregateId),
            cancellationToken);
    }
}