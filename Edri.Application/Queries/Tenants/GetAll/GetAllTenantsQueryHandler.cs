using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Tenants.GetAll;

public sealed class GetAllTenantsQueryHandler :
    IRequestHandler<GetAllTenantsQuery, PagedResult<TenantViewModel>>
{
    private readonly ISortingExpressionProvider<TenantViewModel, Tenant> _sortingExpressionProvider;
    private readonly ITenantRepository _tenantRepository;

    public GetAllTenantsQueryHandler(
        ITenantRepository tenantRepository,
        ISortingExpressionProvider<TenantViewModel, Tenant> sortingExpressionProvider)
    {
        _tenantRepository = tenantRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<TenantViewModel>> Handle(
        GetAllTenantsQuery request,
        CancellationToken cancellationToken)
    {
        var tenantsQuery = _tenantRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.Users.Where(y => request.IncludeDeleted || y.DeletedAt == null))
            .Where(x => request.IncludeDeleted || x.DeletedAt == null );

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            tenantsQuery = tenantsQuery.Where(tenant =>
                tenant.Name.Contains(request.SearchTerm));
        }

        var totalCount = await tenantsQuery.CountAsync(cancellationToken);

        tenantsQuery = tenantsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var tenants = await tenantsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(tenant => TenantViewModel.FromTenant(tenant))
            .ToListAsync(cancellationToken);

        return new PagedResult<TenantViewModel>(
            totalCount, tenants, request.Query.Page, request.Query.PageSize);
    }
}