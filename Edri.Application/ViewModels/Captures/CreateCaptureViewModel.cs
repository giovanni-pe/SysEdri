using System;

namespace Edri.Application.ViewModels.Captures;

public sealed record CreateCaptureViewModel(
    Guid DeviceId,
    DateTime Timestamp,
    string ImageUrl,
    int Status
);