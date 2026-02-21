using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Supply;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class SupplyEventHandler :
    INotificationHandler<SupplyCreatedEvent>,
    INotificationHandler<SupplyDeletedEvent>,
    INotificationHandler<SupplyUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public SupplyEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(SupplyCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(SupplyDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Supply>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(SupplyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Supply>(notification.AggregateId),
            cancellationToken);
    }
}