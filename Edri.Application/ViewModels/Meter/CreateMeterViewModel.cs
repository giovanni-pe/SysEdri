using System;

namespace Edri.Application.ViewModels.Meters;

public sealed record CreateMeterViewModel(
    string MeterNumber,
    Guid SupplyId,
    string? Brand,
    string? Model,
    int Type,
    int? AmperageCapacity,
    DateTime InstallationDate,
    DateTime? LastCalibrationDate,
    int Status
);