namespace Edri.Application.ViewModels.Companies;

public sealed record CreateCompanyViewModel(
    string TaxId,
    string BusinessName,
    string? TradeName,
    string? FiscalAddress,
    string? Phone,
    string? Email,
    string? LogoUrl
);