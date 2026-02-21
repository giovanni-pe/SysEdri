using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;
using MediatR;

namespace Edri.Application.Queries.Tenants.GetAll;

public sealed record GetAllTenantsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<TenantViewModel>>;