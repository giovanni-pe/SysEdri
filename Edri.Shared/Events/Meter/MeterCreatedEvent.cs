using System;

namespace Edri.Shared.Events.Meter;

public sealed class MeterCreatedEvent : DomainEvent
{
    public string MeterNumber { get; set; }
    public Guid SupplyId { get; set; }

    public MeterCreatedEvent(
        Guid meterId,
        string meterNumber,
        Guid supplyId) : base(meterId)
    {
        MeterNumber = meterNumber;
        SupplyId = supplyId;
    }
}