using System;

namespace Edri.Shared.Events.Tariff;

public sealed class TariffUpdatedEvent : DomainEvent
{
    public Guid CompanyId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public TariffUpdatedEvent(
        Guid tariffId,
        Guid companyId,
        string code,
        string name) : base(tariffId)
    {
        CompanyId = companyId;
        Code = code;
        Name = name;
    }
}