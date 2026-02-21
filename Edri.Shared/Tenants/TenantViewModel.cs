using System;

namespace Edri.Shared.Tenants;

public sealed record TenantViewModel(
    Guid Id,
    string Name,
    DateTimeOffset? DeletedAt);