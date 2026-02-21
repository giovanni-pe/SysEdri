using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;

namespace Edri.Application.Interfaces;

public interface IMeterService
{
    Task<Guid> CreateMeterAsync(CreateMeterViewModel meter);
    Task UpdateMeterAsync(UpdateMeterViewModel meter);
    Task DeleteMeterAsync(Guid meterId);
    Task<MeterViewModel?> GetMeterByIdAsync(Guid meterId);
    Task<PagedResult<MeterViewModel>> GetAllMetersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}