using System;

namespace Edri.Application.ViewModels.Tariffs;

public sealed record CreateTariffViewModel(
    Guid CompanyId,
    string Code,
    string Name,
    decimal PricePerKwh,
    decimal FixedCharge,
    string? Description,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    bool IsActive
);