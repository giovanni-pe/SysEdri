using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Devices.GetAll;
using Edri.Application.Queries.Devices.GetDeviceById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;
using Edri.Domain;
using Edri.Domain.Commands.Devices.CreateDevice;
using Edri.Domain.Commands.Devices.DeleteDevice;
using Edri.Domain.Commands.Devices.UpdateDevice;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class DeviceService : IDeviceService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public DeviceService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateDeviceAsync(CreateDeviceViewModel device)
    {
        var deviceId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateDeviceCommand(
            deviceId,
            device.Code,
            device.MeterId,
            device.BranchId,
            device.MacAddress,
            device.Model,
            device.FirmwareVersion,
            device.Status,
            device.LastConnection,
            device.InstallationDate));

        return deviceId;
    }

    public async Task UpdateDeviceAsync(UpdateDeviceViewModel device)
    {
        await _bus.SendCommandAsync(new UpdateDeviceCommand(
            device.Id,
            device.Code,
            device.MeterId,
            device.BranchId,
            device.MacAddress,
            device.Model,
            device.FirmwareVersion,
            device.Status,
            device.LastConnection,
            device.InstallationDate));
    }

    public async Task DeleteDeviceAsync(Guid deviceId)
    {
        await _bus.SendCommandAsync(new DeleteDeviceCommand(deviceId));
    }

    public async Task<DeviceViewModel?> GetDeviceByIdAsync(Guid deviceId)
    {
        var cachedDevice = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Device>(deviceId),
            async () => await _bus.QueryAsync(new GetDeviceByIdQuery(deviceId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedDevice;
    }

    public async Task<PagedResult<DeviceViewModel>> GetAllDevicesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllDevicesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}