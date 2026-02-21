using System;

namespace Edri.Shared.Events.Company;

public sealed class CompanyDeletedEvent : DomainEvent
{
    public CompanyDeletedEvent(Guid companyId) : base(companyId)
    {
    }
}