using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Readings;

public sealed class ReadingViewModel
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Guid? CaptureId { get; set; }
    public string MeterNumber { get; set; } = string.Empty;
    public decimal ValueKwh { get; set; }
    public DateTime ReadingDate { get; set; }
    public int Source { get; set; }
    public string SourceDescription { get; set; } = string.Empty;
    public decimal? OcrConfidence { get; set; }
    public string? Observations { get; set; }

    public static ReadingViewModel FromReading(Reading reading)
    {
        return new ReadingViewModel
        {
            Id = reading.Id,
            DeviceId = reading.DeviceId,
            CaptureId = reading.CaptureId,
            MeterNumber = reading.MeterNumber,
            ValueKwh = reading.ValueKwh,
            ReadingDate = reading.ReadingDate,
            Source = reading.Source,
            SourceDescription = reading.Source switch
            {
                1 => "OCR",
                2 => "Manual",
                3 => "Estimated",
                _ => "Unknown"
            },
            OcrConfidence = reading.OcrConfidence,
            Observations = reading.Observations
        };
    }
}