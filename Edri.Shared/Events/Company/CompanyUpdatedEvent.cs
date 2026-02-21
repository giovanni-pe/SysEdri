using System;

namespace Edri.Shared.Events.Company;

public sealed class CompanyUpdatedEvent : DomainEvent
{
    public string TaxId { get; set; }
    public string BusinessName { get; set; }

    public CompanyUpdatedEvent(
        Guid companyId,
        string taxId,
        string businessName) : base(companyId)
    {
        TaxId = taxId;
        BusinessName = businessName;
    }
}