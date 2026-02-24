using System;

namespace Edri.Shared.Events.Reading;

public sealed class ReadingDeletedEvent : DomainEvent
{
    public ReadingDeletedEvent(Guid readingId) : base(readingId)
    {
    }
}