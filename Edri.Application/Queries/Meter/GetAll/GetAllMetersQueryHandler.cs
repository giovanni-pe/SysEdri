using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Meters.GetAll;

public sealed class GetAllMetersQueryHandler :
    IRequestHandler<GetAllMetersQuery, PagedResult<MeterViewModel>>
{
    private readonly ISortingExpressionProvider<MeterViewModel, Meter> _sortingExpressionProvider;
    private readonly IMeterRepository _meterRepository;

    public GetAllMetersQueryHandler(
        IMeterRepository meterRepository,
        ISortingExpressionProvider<MeterViewModel, Meter> sortingExpressionProvider)
    {
        _meterRepository = meterRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<MeterViewModel>> Handle(
        GetAllMetersQuery request,
        CancellationToken cancellationToken)
    {
        var metersQuery = _meterRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            metersQuery = metersQuery.Where(meter =>
                meter.MeterNumber.Contains(request.SearchTerm) ||
                (meter.Brand != null && meter.Brand.Contains(request.SearchTerm)) ||
                (meter.Model != null && meter.Model.Contains(request.SearchTerm)));
        }

        var totalCount = await metersQuery.CountAsync(cancellationToken);

        metersQuery = metersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var meters = await metersQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(meter => MeterViewModel.FromMeter(meter))
            .ToListAsync(cancellationToken);

        return new PagedResult<MeterViewModel>(
            totalCount, meters, request.Query.Page, request.Query.PageSize);
    }
}