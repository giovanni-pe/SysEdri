using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Provinces.GetAll;

public sealed class GetAllProvincesQueryHandler :
    IRequestHandler<GetAllProvincesQuery, PagedResult<ProvinceViewModel>>
{
    private readonly ISortingExpressionProvider<ProvinceViewModel, Province> _sortingExpressionProvider;
    private readonly IProvinceRepository _ProvinceRepository;

    public GetAllProvincesQueryHandler(
        IProvinceRepository ProvinceRepository,
        ISortingExpressionProvider<ProvinceViewModel, Province> sortingExpressionProvider)
    {
        _ProvinceRepository = ProvinceRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ProvinceViewModel>> Handle(
        GetAllProvincesQuery request,
        CancellationToken cancellationToken)
    {
        var ProvincesQuery = _ProvinceRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null );

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            ProvincesQuery = ProvincesQuery.Where(Province =>
                Province.Name.Contains(request.SearchTerm));
        }

        var totalCount = await ProvincesQuery.CountAsync(cancellationToken);

        ProvincesQuery = ProvincesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Provinces = await ProvincesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(Province => ProvinceViewModel.FromProvince(Province))
            .ToListAsync(cancellationToken);

        return new PagedResult<ProvinceViewModel>(
            totalCount, Provinces, request.Query.Page, request.Query.PageSize);
    }
}