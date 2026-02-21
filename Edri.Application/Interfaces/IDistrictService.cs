using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;

namespace Edri.Application.Interfaces;

public interface IDistrictService
{
    public Task<Guid> CreateDistrictAsync(CreateDistrictViewModel District);
    public Task UpdateDistrictAsync(UpdateDistrictViewModel District);
    public Task DeleteDistrictAsync(Guid DistrictId);
    public Task<DistrictViewModel?> GetDistrictByIdAsync(Guid DistrictId);

    public Task<PagedResult<DistrictViewModel>> GetAllDistrictsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}