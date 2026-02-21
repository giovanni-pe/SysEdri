using System;

namespace Edri.Shared.Events.District;

public sealed class DistrictCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public DistrictCreatedEvent(Guid DistrictId, Guid provinceId, string name,string code) : base(DistrictId)
    {
        Name = name;
    }
}