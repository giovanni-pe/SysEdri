using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;
using MediatR;

namespace Edri.Application.Queries.Captures.GetAll;

public sealed record GetAllCapturesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CaptureViewModel>>;