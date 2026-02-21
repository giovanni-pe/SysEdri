using System;

namespace Edri.Domain.Entities;

public class Tariff : Entity
{
    public Guid CompanyId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public decimal PricePerKwh { get; private set; }
    public decimal FixedCharge { get; private set; }
    public string? Description { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public bool IsActive { get; private set; }

    public virtual Company Company { get; private set; } = null!;

    public Tariff(
        Guid id,
        Guid companyId,
        string code,
        string name,
        decimal pricePerKwh,
        decimal fixedCharge,
        string? description,
        DateTime effectiveFrom,
        DateTime? effectiveTo,
        bool isActive) : base(id)
    {
        CompanyId = companyId;
        Code = code;
        Name = name;
        PricePerKwh = pricePerKwh;
        FixedCharge = fixedCharge;
        Description = description;
        EffectiveFrom = effectiveFrom;
        EffectiveTo = effectiveTo;
        IsActive = isActive;
    }

    public void Update(
        Guid companyId,
        string code,
        string name,
        decimal pricePerKwh,
        decimal fixedCharge,
        string? description,
        DateTime effectiveFrom,
        DateTime? effectiveTo,
        bool isActive)
    {
        CompanyId = companyId;
        Code = code;
        Name = name;
        PricePerKwh = pricePerKwh;
        FixedCharge = fixedCharge;
        Description = description;
        EffectiveFrom = effectiveFrom;
        EffectiveTo = effectiveTo;
        IsActive = isActive;
    }
}