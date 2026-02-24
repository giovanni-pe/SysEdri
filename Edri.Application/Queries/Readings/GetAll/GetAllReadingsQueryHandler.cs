using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Readings.GetAll;

public sealed class GetAllReadingsQueryHandler :
    IRequestHandler<GetAllReadingsQuery, PagedResult<ReadingViewModel>>
{
    private readonly ISortingExpressionProvider<ReadingViewModel, Reading> _sortingExpressionProvider;
    private readonly IReadingRepository _readingRepository;

    public GetAllReadingsQueryHandler(
        IReadingRepository readingRepository,
        ISortingExpressionProvider<ReadingViewModel, Reading> sortingExpressionProvider)
    {
        _readingRepository = readingRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ReadingViewModel>> Handle(
        GetAllReadingsQuery request,
        CancellationToken cancellationToken)
    {
        var readingsQuery = _readingRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            readingsQuery = readingsQuery.Where(reading =>
                reading.MeterNumber.Contains(request.SearchTerm) ||
                (reading.Observations != null && reading.Observations.Contains(request.SearchTerm)));
        }

        var totalCount = await readingsQuery.CountAsync(cancellationToken);

        readingsQuery = readingsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var readings = await readingsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(reading => ReadingViewModel.FromReading(reading))
            .ToListAsync(cancellationToken);

        return new PagedResult<ReadingViewModel>(
            totalCount, readings, request.Query.Page, request.Query.PageSize);
    }
}