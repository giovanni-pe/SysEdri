using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;
using MediatR;

namespace Edri.Application.Queries.Provinces.GetAll;

public sealed record GetAllProvincesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ProvinceViewModel>>;