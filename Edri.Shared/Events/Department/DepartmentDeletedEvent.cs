using System;

namespace Edri.Shared.Events.Department;

public sealed class DepartmentDeletedEvent : DomainEvent
{
    public DepartmentDeletedEvent(Guid DepartmentId) : base(DepartmentId)
    {
    }
}