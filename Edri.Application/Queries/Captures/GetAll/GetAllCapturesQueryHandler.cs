using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Captures.GetAll;

public sealed class GetAllCapturesQueryHandler :
    IRequestHandler<GetAllCapturesQuery, PagedResult<CaptureViewModel>>
{
    private readonly ISortingExpressionProvider<CaptureViewModel, Capture> _sortingExpressionProvider;
    private readonly ICaptureRepository _captureRepository;

    public GetAllCapturesQueryHandler(
        ICaptureRepository captureRepository,
        ISortingExpressionProvider<CaptureViewModel, Capture> sortingExpressionProvider)
    {
        _captureRepository = captureRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CaptureViewModel>> Handle(
        GetAllCapturesQuery request,
        CancellationToken cancellationToken)
    {
        var capturesQuery = _captureRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            capturesQuery = capturesQuery.Where(capture =>
                capture.ImageUrl.Contains(request.SearchTerm));
        }

        var totalCount = await capturesQuery.CountAsync(cancellationToken);

        capturesQuery = capturesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var captures = await capturesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(capture => CaptureViewModel.FromCapture(capture))
            .ToListAsync(cancellationToken);

        return new PagedResult<CaptureViewModel>(
            totalCount, captures, request.Query.Page, request.Query.PageSize);
    }
}