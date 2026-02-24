using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Device;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class DeviceEventHandler :
    INotificationHandler<DeviceCreatedEvent>,
    INotificationHandler<DeviceDeletedEvent>,
    INotificationHandler<DeviceUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public DeviceEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(DeviceCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(DeviceDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Device>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(DeviceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Device>(notification.AggregateId),
            cancellationToken);
    }
}