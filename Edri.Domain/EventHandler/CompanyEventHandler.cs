using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Shared.Events.Company;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Domain.EventHandler;

public sealed class CompanyEventHandler :
    INotificationHandler<CompanyCreatedEvent>,
    INotificationHandler<CompanyDeletedEvent>,
    INotificationHandler<CompanyUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public CompanyEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(CompanyDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Company>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(CompanyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Company>(notification.AggregateId),
            cancellationToken);
    }
}