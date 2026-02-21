using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;
using MediatR;

namespace Edri.Application.Queries.Districts.GetAll;

public sealed record GetAllDistrictsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DistrictViewModel>>;