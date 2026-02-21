using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;

namespace Edri.Application.Interfaces;

public interface ISupplyService
{
    Task<Guid> CreateSupplyAsync(CreateSupplyViewModel supply);
    Task UpdateSupplyAsync(UpdateSupplyViewModel supply);
    Task DeleteSupplyAsync(Guid supplyId);
    Task<SupplyViewModel?> GetSupplyByIdAsync(Guid supplyId);
    Task<PagedResult<SupplyViewModel>> GetAllSuppliesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}