using System;

namespace Edri.Shared.Events.Supply;

public sealed class SupplyDeletedEvent : DomainEvent
{
    public SupplyDeletedEvent(Guid supplyId) : base(supplyId)
    {
    }
}