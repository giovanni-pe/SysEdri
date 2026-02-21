using System;

namespace Edri.Application.ViewModels.Branches;

public sealed record CreateBranchViewModel(
    Guid CompanyId,
    Guid DistrictId,
    string Name,
    string? Address,
    string? Phone,
    bool IsActive
);