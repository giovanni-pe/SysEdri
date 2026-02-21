using System;

namespace Edri.Shared.Events.District;

public sealed class DistrictDeletedEvent : DomainEvent
{
    public DistrictDeletedEvent(Guid DistrictId) : base(DistrictId)
    {
    }
}