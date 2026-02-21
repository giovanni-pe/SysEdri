using System;

namespace Edri.Shared.Events.Supply;

public sealed class SupplyCreatedEvent : DomainEvent
{
    public string SupplyNumber { get; set; }
    public Guid CustomerId { get; set; }

    public SupplyCreatedEvent(
        Guid supplyId,
        string supplyNumber,
        Guid customerId) : base(supplyId)
    {
        SupplyNumber = supplyNumber;
        CustomerId = customerId;
    }
}