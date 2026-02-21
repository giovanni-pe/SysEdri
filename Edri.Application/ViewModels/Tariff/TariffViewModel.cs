using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Tariffs;

public sealed class TariffViewModel
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal PricePerKwh { get; set; }
    public decimal FixedCharge { get; set; }
    public string? Description { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsActive { get; set; }

    public static TariffViewModel FromTariff(Tariff tariff)
    {
        return new TariffViewModel
        {
            Id = tariff.Id,
            CompanyId = tariff.CompanyId,
            Code = tariff.Code,
            Name = tariff.Name,
            PricePerKwh = tariff.PricePerKwh,
            FixedCharge = tariff.FixedCharge,
            Description = tariff.Description,
            EffectiveFrom = tariff.EffectiveFrom,
            EffectiveTo = tariff.EffectiveTo,
            IsActive = tariff.IsActive
        };
    }
}