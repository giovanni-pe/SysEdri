using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;
using MediatR;

namespace Edri.Application.Queries.Devices.GetAll;

public sealed record GetAllDevicesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DeviceViewModel>>;