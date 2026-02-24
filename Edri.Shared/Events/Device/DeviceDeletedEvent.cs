using System;

namespace Edri.Shared.Events.Device;

public sealed class DeviceDeletedEvent : DomainEvent
{
    public DeviceDeletedEvent(Guid deviceId) : base(deviceId)
    {
    }
}