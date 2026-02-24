using System;

namespace Edri.Application.ViewModels.Captures;

public sealed record UpdateCaptureViewModel(
    Guid Id,
    Guid DeviceId,
    DateTime Timestamp,
    string ImageUrl,
    int Status
);