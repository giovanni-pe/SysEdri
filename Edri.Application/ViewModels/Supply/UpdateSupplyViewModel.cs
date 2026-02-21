using System;

namespace Edri.Application.ViewModels.Supplies;

public sealed record UpdateSupplyViewModel(
    Guid Id,
    string SupplyNumber,
    Guid CustomerId,
    Guid TariffId,
    Guid BranchId,
    Guid DistrictId,
    string InstallationAddress,
    string? Reference,
    decimal? Latitude,
    decimal? Longitude,
    string Status,
    DateTime ActivationDate,
    DateTime? TerminationDate
);