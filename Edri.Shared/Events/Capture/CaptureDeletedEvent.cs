using System;

namespace Edri.Shared.Events.Capture;

public sealed class CaptureDeletedEvent : DomainEvent
{
    public CaptureDeletedEvent(Guid captureId) : base(captureId)
    {
    }
}