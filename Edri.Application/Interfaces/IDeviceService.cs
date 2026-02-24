using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;

namespace Edri.Application.Interfaces;

public interface IDeviceService
{
    Task<Guid> CreateDeviceAsync(CreateDeviceViewModel device);
    Task UpdateDeviceAsync(UpdateDeviceViewModel device);
    Task DeleteDeviceAsync(Guid deviceId);
    Task<DeviceViewModel?> GetDeviceByIdAsync(Guid deviceId);
    Task<PagedResult<DeviceViewModel>> GetAllDevicesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}