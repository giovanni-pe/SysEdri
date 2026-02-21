using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;
using MediatR;

namespace Edri.Application.Queries.Supplies.GetAll;

public sealed record GetAllSuppliesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<SupplyViewModel>>;