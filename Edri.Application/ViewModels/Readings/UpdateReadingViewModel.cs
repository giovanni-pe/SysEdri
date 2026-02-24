using System;

namespace Edri.Application.ViewModels.Readings;

public sealed record UpdateReadingViewModel(
    Guid Id,
    Guid DeviceId,
    Guid? CaptureId,
    string MeterNumber,
    decimal ValueKwh,
    DateTime ReadingDate,
    int Source,
    decimal? OcrConfidence,
    string? Observations
);