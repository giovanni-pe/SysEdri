using System;

namespace Edri.Domain.Entities;

public class Company : Entity
{
    public string TaxId { get; private set; }
    public string BusinessName { get; private set; }
    public string? TradeName { get; private set; }
    public string? FiscalAddress { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? LogoUrl { get; private set; }

    public Company(
        Guid id,
        string taxId,
        string businessName,
        string? tradeName,
        string? fiscalAddress,
        string? phone,
        string? email,
        string? logoUrl) : base(id)
    {
        TaxId = taxId;
        BusinessName = businessName;
        TradeName = tradeName;
        FiscalAddress = fiscalAddress;
        Phone = phone;
        Email = email;
        LogoUrl = logoUrl;
    }

    public void Update(
        string taxId,
        string businessName,
        string? tradeName,
        string? fiscalAddress,
        string? phone,
        string? email,
        string? logoUrl)
    {
        TaxId = taxId;
        BusinessName = businessName;
        TradeName = tradeName;
        FiscalAddress = fiscalAddress;
        Phone = phone;
        Email = email;
        LogoUrl = logoUrl;
    }
}