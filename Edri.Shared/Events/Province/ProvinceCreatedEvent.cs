using System;

namespace Edri.Shared.Events.Province;

public sealed class ProvinceCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ProvinceCreatedEvent(Guid ProvinceId, Guid departmentId, string name,string code) : base(ProvinceId)
    {
        Name = name;
    }
}