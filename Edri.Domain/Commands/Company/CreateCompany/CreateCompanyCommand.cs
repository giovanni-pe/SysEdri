using System;

namespace Edri.Domain.Commands.Companies.CreateCompany;

public sealed class CreateCompanyCommand : CommandBase
{
    private static readonly CreateCompanyCommandValidation s_validation = new();

    public string TaxId { get; }
    public string BusinessName { get; }
    public string? TradeName { get; }
    public string? FiscalAddress { get; }
    public string? Phone { get; }
    public string? Email { get; }
    public string? LogoUrl { get; }

    public CreateCompanyCommand(
        Guid companyId,
        string taxId,
        string businessName,
        string? tradeName,
        string? fiscalAddress,
        string? phone,
        string? email,
        string? logoUrl) : base(companyId)
    {
        TaxId = taxId;
        BusinessName = businessName;
        TradeName = tradeName;
        FiscalAddress = fiscalAddress;
        Phone = phone;
        Email = email;
        LogoUrl = logoUrl;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}