using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Tariffs.GetAll;

public sealed class GetAllTariffsQueryHandler :
    IRequestHandler<GetAllTariffsQuery, PagedResult<TariffViewModel>>
{
    private readonly ISortingExpressionProvider<TariffViewModel, Tariff> _sortingExpressionProvider;
    private readonly ITariffRepository _tariffRepository;

    public GetAllTariffsQueryHandler(
        ITariffRepository tariffRepository,
        ISortingExpressionProvider<TariffViewModel, Tariff> sortingExpressionProvider)
    {
        _tariffRepository = tariffRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<TariffViewModel>> Handle(
        GetAllTariffsQuery request,
        CancellationToken cancellationToken)
    {
        var tariffsQuery = _tariffRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            tariffsQuery = tariffsQuery.Where(tariff =>
                tariff.Code.Contains(request.SearchTerm) ||
                tariff.Name.Contains(request.SearchTerm) ||
                (tariff.Description != null && tariff.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await tariffsQuery.CountAsync(cancellationToken);

        tariffsQuery = tariffsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var tariffs = await tariffsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(tariff => TariffViewModel.FromTariff(tariff))
            .ToListAsync(cancellationToken);

        return new PagedResult<TariffViewModel>(
            totalCount, tariffs, request.Query.Page, request.Query.PageSize);
    }
}