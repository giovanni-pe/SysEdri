using System;

namespace Edri.Shared.Events.Meter;

public sealed class MeterUpdatedEvent : DomainEvent
{
    public string MeterNumber { get; set; }
    public Guid SupplyId { get; set; }

    public MeterUpdatedEvent(
        Guid meterId,
        string meterNumber,
        Guid supplyId) : base(meterId)
    {
        MeterNumber = meterNumber;
        SupplyId = supplyId;
    }
}