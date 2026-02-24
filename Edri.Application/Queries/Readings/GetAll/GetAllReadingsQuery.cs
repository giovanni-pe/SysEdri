using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;
using MediatR;

namespace Edri.Application.Queries.Readings.GetAll;

public sealed record GetAllReadingsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ReadingViewModel>>;