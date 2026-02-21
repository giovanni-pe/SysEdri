using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Companies;

public sealed class CompanyViewModel
{
    public Guid Id { get; set; }
    public string TaxId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string? TradeName { get; set; }
    public string? FiscalAddress { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? LogoUrl { get; set; }

    public static CompanyViewModel FromCompany(Company company)
    {
        return new CompanyViewModel
        {
            Id = company.Id,
            TaxId = company.TaxId,
            BusinessName = company.BusinessName,
            TradeName = company.TradeName,
            FiscalAddress = company.FiscalAddress,
            Phone = company.Phone,
            Email = company.Email,
            LogoUrl = company.LogoUrl
        };
    }
}