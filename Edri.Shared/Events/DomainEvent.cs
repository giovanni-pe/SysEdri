using System;
using MassTransit;
using MediatR;

namespace Edri.Shared.Events;

[ExcludeFromTopology]
public abstract class DomainEvent : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected DomainEvent(Guid aggregateId) : base(aggregateId)
    {
        // CAMBIO: Usar UtcNow para evitar el error de PostgreSQL
        Timestamp = DateTime.UtcNow;
    }

    protected DomainEvent(Guid aggregateId, string? messageType) : base(aggregateId, messageType)
    {
        // CAMBIO: Usar UtcNow
        Timestamp = DateTime.UtcNow;
    }
}