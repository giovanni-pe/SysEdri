using System;

namespace Edri.Shared.Events.Reading;

public sealed class ReadingUpdatedEvent : DomainEvent
{
    public string MeterNumber { get; set; }
    public decimal ValueKwh { get; set; }

    public ReadingUpdatedEvent(
        Guid readingId,
        string meterNumber,
        decimal valueKwh) : base(readingId)
    {
        MeterNumber = meterNumber;
        ValueKwh = valueKwh;
    }
}