using System;

namespace Edri.Shared.Events.Department;

public sealed class DepartmentUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public DepartmentUpdatedEvent(Guid DepartmentId, string name,string code) : base(DepartmentId)
    {
        Name = name;
    }
}