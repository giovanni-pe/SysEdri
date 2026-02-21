using System;

namespace Edri.Shared.Events.Meter;

public sealed class MeterDeletedEvent : DomainEvent
{
    public MeterDeletedEvent(Guid meterId) : base(meterId)
    {
    }
}