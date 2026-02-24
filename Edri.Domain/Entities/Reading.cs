using System;

namespace Edri.Domain.Entities;

public class Reading : Entity
{
    public Guid DeviceId { get; private set; }
    public Guid? CaptureId { get; private set; }
    public string MeterNumber { get; private set; }
    public decimal ValueKwh { get; private set; }
    public DateTime ReadingDate { get; private set; }
    public int Source { get; private set; }
    public decimal? OcrConfidence { get; private set; }
    public string? Observations { get; private set; }

    public virtual Device Device { get; private set; } = null!;
    public virtual Capture? Capture { get; private set; }

    public Reading(
        Guid id,
        Guid deviceId,
        Guid? captureId,
        string meterNumber,
        decimal valueKwh,
        DateTime readingDate,
        int source,
        decimal? ocrConfidence,
        string? observations) : base(id)
    {
        DeviceId = deviceId;
        CaptureId = captureId;
        MeterNumber = meterNumber;
        ValueKwh = valueKwh;
        ReadingDate = readingDate;
        Source = source;
        OcrConfidence = ocrConfidence;
        Observations = observations;
    }

    public void Update(
        Guid deviceId,
        Guid? captureId,
        string meterNumber,
        decimal valueKwh,
        DateTime readingDate,
        int source,
        decimal? ocrConfidence,
        string? observations)
    {
        DeviceId = deviceId;
        CaptureId = captureId;
        MeterNumber = meterNumber;
        ValueKwh = valueKwh;
        ReadingDate = readingDate;
        Source = source;
        OcrConfidence = ocrConfidence;
        Observations = observations;
    }
}