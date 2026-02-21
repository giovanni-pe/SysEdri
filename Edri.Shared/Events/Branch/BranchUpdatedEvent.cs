using System;

namespace Edri.Shared.Events.Branch;

public sealed class BranchUpdatedEvent : DomainEvent
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }

    public BranchUpdatedEvent(
        Guid branchId,
        Guid companyId,
        string name) : base(branchId)
    {
        CompanyId = companyId;
        Name = name;
    }
}