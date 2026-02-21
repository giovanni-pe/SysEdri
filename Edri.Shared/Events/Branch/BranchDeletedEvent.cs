using System;

namespace Edri.Shared.Events.Branch;

public sealed class BranchDeletedEvent : DomainEvent
{
    public BranchDeletedEvent(Guid branchId) : base(branchId)
    {
    }
}