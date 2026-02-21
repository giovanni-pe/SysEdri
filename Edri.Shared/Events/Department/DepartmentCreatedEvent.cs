using System;

namespace Edri.Shared.Events.Department;

public sealed class DepartmentCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public DepartmentCreatedEvent(Guid DepartmentId, string name,string code) : base(DepartmentId)
    {
        Name = name;
    }
}