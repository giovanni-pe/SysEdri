using System;

namespace Edri.Shared.Events.Device;

public sealed class DeviceUpdatedEvent : DomainEvent
{
    public string Code { get; set; }
    public Guid MeterId { get; set; }

    public DeviceUpdatedEvent(
        Guid deviceId,
        string code,
        Guid meterId) : base(deviceId)
    {
        Code = code;
        MeterId = meterId;
    }
}