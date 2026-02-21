using System;

namespace Edri.Application.ViewModels.Tenants;

public sealed record UpdateTenantViewModel(
    Guid Id,
    string Name);