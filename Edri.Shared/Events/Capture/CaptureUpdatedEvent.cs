using System;

namespace Edri.Shared.Events.Capture;

public sealed class CaptureUpdatedEvent : DomainEvent
{
    public Guid DeviceId { get; set; }
    public int Status { get; set; }

    public CaptureUpdatedEvent(
        Guid captureId,
        Guid deviceId,
        int status) : base(captureId)
    {
        DeviceId = deviceId;
        Status = status;
    }
}