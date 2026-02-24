using System;

namespace Edri.Application.ViewModels.Devices;

public sealed record CreateDeviceViewModel(
    string Code,
    Guid MeterId,
    Guid BranchId,
    string? MacAddress,
    string? Model,
    string? FirmwareVersion,
    int Status,
    DateTime? LastConnection,
    DateTime InstallationDate
);