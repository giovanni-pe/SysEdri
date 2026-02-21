using System;

namespace Edri.Domain.Commands.Tariffs.CreateTariff;

public sealed class CreateTariffCommand : CommandBase
{
    private static readonly CreateTariffCommandValidation s_validation = new();

    public Guid CompanyId { get; }
    public string Code { get; }
    public string Name { get; }
    public decimal PricePerKwh { get; }
    public decimal FixedCharge { get; }
    public string? Description { get; }
    public DateTime EffectiveFrom { get; }
    public DateTime? EffectiveTo { get; }
    public bool IsActive { get; }

    public CreateTariffCommand(
        Guid tariffId,
        Guid companyId,
        string code,
        string name,
        decimal pricePerKwh,
        decimal fixedCharge,
        string? description,
        DateTime effectiveFrom,
        DateTime? effectiveTo,
        bool isActive) : base(tariffId)
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

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}