using System;

namespace Edri.Shared.Events.Tariff;

public sealed class TariffDeletedEvent : DomainEvent
{
    public TariffDeletedEvent(Guid tariffId) : base(tariffId)
    {
    }
}