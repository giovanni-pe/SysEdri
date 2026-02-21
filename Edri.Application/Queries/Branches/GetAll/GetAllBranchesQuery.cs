using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;
using MediatR;

namespace Edri.Application.Queries.Branches.GetAll;

public sealed record GetAllBranchesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<BranchViewModel>>;