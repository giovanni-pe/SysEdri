using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;

namespace Edri.Application.Interfaces;

public interface IBranchService
{
    Task<Guid> CreateBranchAsync(CreateBranchViewModel branch);
    Task UpdateBranchAsync(UpdateBranchViewModel branch);
    Task DeleteBranchAsync(Guid branchId);
    Task<BranchViewModel?> GetBranchByIdAsync(Guid branchId);
    Task<PagedResult<BranchViewModel>> GetAllBranchesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}