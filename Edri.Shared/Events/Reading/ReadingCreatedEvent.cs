using System;

namespace Edri.Shared.Events.Reading;

public sealed class ReadingCreatedEvent : DomainEvent
{
    public string MeterNumber { get; set; }
    public decimal ValueKwh { get; set; }
    public int Source { get; set; }

    public ReadingCreatedEvent(
        Guid readingId,
        string meterNumber,
        decimal valueKwh,
        int source) : base(readingId)
    {
        MeterNumber = meterNumber;
        ValueKwh = valueKwh;
        Source = source;
    }
}