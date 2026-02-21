using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;
using MediatR;

namespace Edri.Application.Queries.Meters.GetAll;

public sealed record GetAllMetersQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<MeterViewModel>>;