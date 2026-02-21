using System.Threading.Tasks;
using Edri.Shared.Events.Tenant;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Edri.Domain.Consumers;

public sealed class TenantUpdatedEventConsumer : IConsumer<TenantUpdatedEvent>
{
    private readonly ILogger<TenantUpdatedEventConsumer> _logger;

    public TenantUpdatedEventConsumer(ILogger<TenantUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TenantUpdatedEvent> context)
    {
        _logger.LogInformation("TenantUpdatedEventConsumer: {TenantId}", context.Message.AggregateId);
        return Task.CompletedTask;
    }
}