using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Branches.GetAll;

public sealed class GetAllBranchesQueryHandler :
    IRequestHandler<GetAllBranchesQuery, PagedResult<BranchViewModel>>
{
    private readonly ISortingExpressionProvider<BranchViewModel, Branch> _sortingExpressionProvider;
    private readonly IBranchRepository _branchRepository;

    public GetAllBranchesQueryHandler(
        IBranchRepository branchRepository,
        ISortingExpressionProvider<BranchViewModel, Branch> sortingExpressionProvider)
    {
        _branchRepository = branchRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<BranchViewModel>> Handle(
        GetAllBranchesQuery request,
        CancellationToken cancellationToken)
    {
        var branchesQuery = _branchRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            branchesQuery = branchesQuery.Where(branch =>
                branch.Name.Contains(request.SearchTerm) ||
                (branch.Address != null && branch.Address.Contains(request.SearchTerm)));
        }

        var totalCount = await branchesQuery.CountAsync(cancellationToken);

        branchesQuery = branchesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var branches = await branchesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(branch => BranchViewModel.FromBranch(branch))
            .ToListAsync(cancellationToken);

        return new PagedResult<BranchViewModel>(
            totalCount, branches, request.Query.Page, request.Query.PageSize);
    }
}