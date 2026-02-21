using System;

namespace Edri.Application.ViewModels.Customers;

public sealed record UpdateCustomerViewModel(
    Guid Id,
    Guid UserId,
    Guid DistrictId,
    string DocumentType,
    string DocumentNumber,
    string CustomerType,
    string? BusinessName,
    string? FirstName,
    string? LastName,
    string? Address,
    string? Phone,
    string? Email,
    string Status
);