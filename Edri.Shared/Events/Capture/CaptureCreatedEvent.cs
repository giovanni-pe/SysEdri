using System;

namespace Edri.Shared.Events.Capture;

public sealed class CaptureCreatedEvent : DomainEvent
{
    public Guid DeviceId { get; set; }
    public string ImageUrl { get; set; }

    public CaptureCreatedEvent(
        Guid captureId,
        Guid deviceId,
        string imageUrl) : base(captureId)
    {
        DeviceId = deviceId;
        ImageUrl = imageUrl;
    }
}