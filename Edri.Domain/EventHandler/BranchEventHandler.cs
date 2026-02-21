using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Branch;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class BranchEventHandler :
    INotificationHandler<BranchCreatedEvent>,
    INotificationHandler<BranchDeletedEvent>,
    INotificationHandler<BranchUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public BranchEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(BranchCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(BranchDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Branch>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(BranchUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Branch>(notification.AggregateId),
            cancellationToken);
    }
}