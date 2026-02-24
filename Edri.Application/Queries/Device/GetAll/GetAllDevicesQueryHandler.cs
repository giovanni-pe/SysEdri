using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Devices.GetAll;

public sealed class GetAllDevicesQueryHandler :
    IRequestHandler<GetAllDevicesQuery, PagedResult<DeviceViewModel>>
{
    private readonly ISortingExpressionProvider<DeviceViewModel, Device> _sortingExpressionProvider;
    private readonly IDeviceRepository _deviceRepository;

    public GetAllDevicesQueryHandler(
        IDeviceRepository deviceRepository,
        ISortingExpressionProvider<DeviceViewModel, Device> sortingExpressionProvider)
    {
        _deviceRepository = deviceRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DeviceViewModel>> Handle(
        GetAllDevicesQuery request,
        CancellationToken cancellationToken)
    {
        var devicesQuery = _deviceRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            devicesQuery = devicesQuery.Where(device =>
                device.Code.Contains(request.SearchTerm) ||
                (device.MacAddress != null && device.MacAddress.Contains(request.SearchTerm)) ||
                (device.Model != null && device.Model.Contains(request.SearchTerm)));
        }

        var totalCount = await devicesQuery.CountAsync(cancellationToken);

        devicesQuery = devicesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var devices = await devicesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(device => DeviceViewModel.FromDevice(device))
            .ToListAsync(cancellationToken);

        return new PagedResult<DeviceViewModel>(
            totalCount, devices, request.Query.Page, request.Query.PageSize);
    }
}