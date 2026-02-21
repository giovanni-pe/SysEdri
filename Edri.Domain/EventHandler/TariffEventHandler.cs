using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Tariff;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class TariffEventHandler :
    INotificationHandler<TariffCreatedEvent>,
    INotificationHandler<TariffDeletedEvent>,
    INotificationHandler<TariffUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public TariffEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(TariffCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(TariffDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Tariff>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(TariffUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Tariff>(notification.AggregateId),
            cancellationToken);
    }
}