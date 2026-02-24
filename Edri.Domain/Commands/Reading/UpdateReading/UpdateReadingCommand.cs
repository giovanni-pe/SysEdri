using System;

namespace Edri.Domain.Commands.Readings.UpdateReading;

public sealed class UpdateReadingCommand : CommandBase
{
    private static readonly UpdateReadingCommandValidation s_validation = new();

    public Guid DeviceId { get; }
    public Guid? CaptureId { get; }
    public string MeterNumber { get; }
    public decimal ValueKwh { get; }
    public DateTime ReadingDate { get; }
    public int Source { get; }
    public decimal? OcrConfidence { get; }
    public string? Observations { get; }

    public UpdateReadingCommand(
        Guid readingId,
        Guid deviceId,
        Guid? captureId,
        string meterNumber,
        decimal valueKwh,
        DateTime readingDate,
        int source,
        decimal? ocrConfidence,
        string? observations) : base(readingId)
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

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}