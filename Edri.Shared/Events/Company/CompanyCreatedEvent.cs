using System;

namespace Edri.Shared.Events.Company;

public sealed class CompanyCreatedEvent : DomainEvent
{
    public string TaxId { get; set; }
    public string BusinessName { get; set; }

    public CompanyCreatedEvent(
        Guid companyId,
        string taxId,
        string businessName) : base(companyId)
    {
        TaxId = taxId;
        BusinessName = businessName;
    }
}