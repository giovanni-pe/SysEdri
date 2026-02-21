using System;

namespace Edri.Shared.Events.Supply;

public sealed class SupplyUpdatedEvent : DomainEvent
{
    public string SupplyNumber { get; set; }
    public Guid CustomerId { get; set; }

    public SupplyUpdatedEvent(
        Guid supplyId,
        string supplyNumber,
        Guid customerId) : base(supplyId)
    {
        SupplyNumber = supplyNumber;
        CustomerId = customerId;
    }
}