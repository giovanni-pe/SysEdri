using System;

namespace Edri.Application.ViewModels.Companies;

public sealed record UpdateCompanyViewModel(
    Guid Id,
    string TaxId,
    string BusinessName,
    string? TradeName,
    string? FiscalAddress,
    string? Phone,
    string? Email,
    string? LogoUrl
);