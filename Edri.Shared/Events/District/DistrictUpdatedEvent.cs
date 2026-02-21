using System;

namespace Edri.Shared.Events.District;

public sealed class DistrictUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public DistrictUpdatedEvent(Guid DistrictId,Guid    provinceId, string name,string code) : base(DistrictId)
    {
        Name = name;
    }
}