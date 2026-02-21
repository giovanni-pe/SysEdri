using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;

namespace Edri.Application.Interfaces;

public interface IProvinceService
{
    public Task<Guid> CreateProvinceAsync(CreateProvinceViewModel Province);
    public Task UpdateProvinceAsync(UpdateProvinceViewModel Province);
    public Task DeleteProvinceAsync(Guid ProvinceId);
    public Task<ProvinceViewModel?> GetProvinceByIdAsync(Guid ProvinceId);

    public Task<PagedResult<ProvinceViewModel>> GetAllProvincesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}