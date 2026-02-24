using System;

namespace Edri.Application.ViewModels.Readings;

public sealed record CreateReadingViewModel(
    Guid DeviceId,
    Guid? CaptureId,
    string MeterNumber,
    decimal ValueKwh,
    DateTime ReadingDate,
    int Source,
    decimal? OcrConfidence,
    string? Observations
);