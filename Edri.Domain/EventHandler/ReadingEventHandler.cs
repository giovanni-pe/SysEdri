using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Reading;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class ReadingEventHandler :
    INotificationHandler<ReadingCreatedEvent>,
    INotificationHandler<ReadingDeletedEvent>,
    INotificationHandler<ReadingUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ReadingEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(ReadingCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ReadingDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Reading>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(ReadingUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Reading>(notification.AggregateId),
            cancellationToken);
    }
}