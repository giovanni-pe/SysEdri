using System;

namespace Edri.Shared.Events.Province;

public sealed class ProvinceUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ProvinceUpdatedEvent(Guid ProvinceId,Guid    departmentId, string name,string code) : base(ProvinceId)
    {
        Name = name;
    }
}