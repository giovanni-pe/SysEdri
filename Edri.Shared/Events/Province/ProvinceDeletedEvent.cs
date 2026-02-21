using System;

namespace Edri.Shared.Events.Province;

public sealed class ProvinceDeletedEvent : DomainEvent
{
    public ProvinceDeletedEvent(Guid ProvinceId) : base(ProvinceId)
    {
    }
}