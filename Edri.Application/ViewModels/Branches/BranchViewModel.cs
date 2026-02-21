using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Branches;

public sealed class BranchViewModel
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid DistrictId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }

    public static BranchViewModel FromBranch(Branch branch)
    {
        return new BranchViewModel
        {
            Id = branch.Id,
            CompanyId = branch.CompanyId,
            DistrictId = branch.DistrictId,
            Name = branch.Name,
            Address = branch.Address,
            Phone = branch.Phone,
            IsActive = branch.IsActive
        };
    }
}