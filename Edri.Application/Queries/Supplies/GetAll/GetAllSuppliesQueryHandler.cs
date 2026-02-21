using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Supplies.GetAll;

public sealed class GetAllSuppliesQueryHandler :
    IRequestHandler<GetAllSuppliesQuery, PagedResult<SupplyViewModel>>
{
    private readonly ISortingExpressionProvider<SupplyViewModel, Supply> _sortingExpressionProvider;
    private readonly ISupplyRepository _supplyRepository;

    public GetAllSuppliesQueryHandler(
        ISupplyRepository supplyRepository,
        ISortingExpressionProvider<SupplyViewModel, Supply> sortingExpressionProvider)
    {
        _supplyRepository = supplyRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<SupplyViewModel>> Handle(
        GetAllSuppliesQuery request,
        CancellationToken cancellationToken)
    {
        var suppliesQuery = _supplyRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            suppliesQuery = suppliesQuery.Where(supply =>
                supply.SupplyNumber.Contains(request.SearchTerm) ||
                supply.InstallationAddress.Contains(request.SearchTerm) ||
                (supply.Reference != null && supply.Reference.Contains(request.SearchTerm)));
        }

        var totalCount = await suppliesQuery.CountAsync(cancellationToken);

        suppliesQuery = suppliesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var supplies = await suppliesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(supply => SupplyViewModel.FromSupply(supply))
            .ToListAsync(cancellationToken);

        return new PagedResult<SupplyViewModel>(
            totalCount, supplies, request.Query.Page, request.Query.PageSize);
    }
}