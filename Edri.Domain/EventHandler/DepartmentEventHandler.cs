using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Department;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class DepartmentEventHandler :
    INotificationHandler<DepartmentCreatedEvent>,
    INotificationHandler<DepartmentDeletedEvent>,
    INotificationHandler<DepartmentUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public DepartmentEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(DepartmentDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Department>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(DepartmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Department>(notification.AggregateId),
            cancellationToken);
    }
}