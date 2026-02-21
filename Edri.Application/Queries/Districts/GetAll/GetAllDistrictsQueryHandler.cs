using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Districts.GetAll;

public sealed class GetAllDistrictsQueryHandler :
    IRequestHandler<GetAllDistrictsQuery, PagedResult<DistrictViewModel>>
{
    private readonly ISortingExpressionProvider<DistrictViewModel, District> _sortingExpressionProvider;
    private readonly IDistrictRepository _DistrictRepository;

    public GetAllDistrictsQueryHandler(
        IDistrictRepository DistrictRepository,
        ISortingExpressionProvider<DistrictViewModel, District> sortingExpressionProvider)
    {
        _DistrictRepository = DistrictRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DistrictViewModel>> Handle(
        GetAllDistrictsQuery request,
        CancellationToken cancellationToken)
    {
        var DistrictsQuery = _DistrictRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null );

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            DistrictsQuery = DistrictsQuery.Where(District =>
                District.Name.Contains(request.SearchTerm));
        }

        var totalCount = await DistrictsQuery.CountAsync(cancellationToken);

        DistrictsQuery = DistrictsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Districts = await DistrictsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(District => DistrictViewModel.FromDistrict(District))
            .ToListAsync(cancellationToken);

        return new PagedResult<DistrictViewModel>(
            totalCount, Districts, request.Query.Page, request.Query.PageSize);
    }
}