using System;

namespace Edri.Shared.Events.Branch;

public sealed class BranchCreatedEvent : DomainEvent
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }

    public BranchCreatedEvent(
        Guid branchId,
        Guid companyId,
        string name) : base(branchId)
    {
        CompanyId = companyId;
        Name = name;
    }
}